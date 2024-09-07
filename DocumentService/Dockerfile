FROM python:3.11
EXPOSE 80

WORKDIR /code

COPY ./RentalManager.DocumentService/requirements.txt /code/requirements.txt

RUN pip install --no-cache-dir --upgrade -r /code/requirements.txt

COPY ./RentalManager.DocumentService /code/

CMD ["uvicorn", "DocumentService:app", "--host", "0.0.0.0", "--port", "80"]