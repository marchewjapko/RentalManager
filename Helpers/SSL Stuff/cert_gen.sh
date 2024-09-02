#!/bin/bash

usage() {
  echo "Usage"
  echo -e "  cert_gen [options] \n"
  echo "Options:"
  echo -e "   -n \t name of the certificate to create"
  echo -e "   -c \t name of the file containing CA certificate"
  echo -e "   -r \t route to your CA certificate, optional defaults to current directory"
  echo -e "   -d \t DNS name of your domain\n"
  echo -e "Example:"
  echo -e "   ./cert_gen.sh -n home.local -c root-ca -r "./SSL/CA" -d *home.local"

  exit 1
}

validate_file_existance() {
  if ! [ -f "$1" ]; then
    echo -e "No such file:"
    echo -e "  $1"
    exit 1
  fi
}

while getopts ":n:c:d:h:r:" option; do
  case "$option" in
    n) new_certificate_name=${OPTARG};;
    c) ca_certificate_name=${OPTARG};;
    r) route_to_ca_certificate=${OPTARG};;
    d) dns_name=${OPTARG};;
    h) usage;;
    *) echo -e "Unrecognized option $OPTARG";usage;
  esac
done

if [ -z "${new_certificate_name}" ] || [ -z "${ca_certificate_name}" ] || [ -z "${dns_name}" ]; then
  echo -e "Error: invalid usage"
  usage
fi

if [ -z "${route_to_ca_certificate}" ] ; then
  route_to_ca_certificate="."
fi

validate_file_existance "$route_to_ca_certificate/$ca_certificate_name.crt"
validate_file_existance "$route_to_ca_certificate/$ca_certificate_name.key"

openssl req -nodes   \
  -newkey rsa:2048   \
  -keyout "$new_certificate_name.key" \
  -out "$new_certificate_name.csr"    \
  -subj '/C=PL/ST=Mazovia/L=Warsaw/O=JoeyAuthentik/CN=joey_cn' \
  2>/dev/null

openssl x509 -req    \
  -CA "$route_to_ca_certificate/$ca_certificate_name.crt"    \
  -CAkey "$route_to_ca_certificate/$ca_certificate_name.key" \
  -in "$new_certificate_name.csr"     \
  -out "$new_certificate_name.crt"    \
  -days 365          \
  -CAcreateserial    \
  -extfile <(printf "
    subjectAltName = DNS:$dns_name \n
    authorityKeyIdentifier = keyid,issuer \n
    basicConstraints = CA:FALSE \n
    keyUsage = digitalSignature, keyEncipherment \n
    extendedKeyUsage=serverAuth"
  )

echo -e "Created a new certificate for domain ${dns_name}:"
echo -e "  - $new_certificate_name.key"
echo -e "  - $new_certificate_name.crt"