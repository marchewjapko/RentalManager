from reportlab.lib import colors
from reportlab.pdfgen import canvas
import tempfile
import os
import uuid
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.platypus import TableStyle, Table
from Models import Agreement

pdfmetrics.registerFont(TTFont('RobotoRegular', './Fonts/Roboto-Regular.ttf'))
pdfmetrics.registerFont(TTFont('RobotoBold', './Fonts/Roboto-Bold.ttf'))


def generate_pdf(arg: Agreement):
    temp_dir = tempfile.gettempdir()
    file_name = str(uuid.uuid4()) + '.pdf'
    file_path = os.path.join(temp_dir, file_name)
    c = canvas.Canvas(file_path)
    c.setFont("RobotoRegular", 12)
    c.setLineWidth(2)

    c.drawString(40, 790, 'Zawarta pomiędzy')
    c.line(35, 785, 230, 785)

    c.drawString(40, 765, 'Janex Jakub Jankowski')
    c.drawString(40, 750, '3 Maja 21G')
    c.drawString(40, 735, '05-827 Grodzisk Mazowiecki')
    c.drawString(40, 720, 'NIP: +48 741-158-53-07')
    c.drawString(40, 705, 'TEL: 505-070-546')

    # ---------------------------------------------------

    c.drawString(40, 670, 'a wypożyczającym')
    c.line(35, 665, 230, 665)

    c.drawString(40, 645, 'Imię i nazwisko:')
    c.drawString(40, 625, 'Adres zamieszkania:')
    c.drawString(40, 605, 'Seria i nr. dowodu osobistego:')
    c.drawString(40, 585, 'Tel. kontaktowy:')

    test_indent = pdfmetrics.stringWidth(
        'Seria inr. dowodu osobistego:', 'RobotoRegular', 12) + 40 + 20
    c.drawString(test_indent, 645, arg.client.name + ' ' + arg.client.surname)
    c.setLineWidth(0.5)
    c.line(test_indent, 643, 400, 643)
    c.drawString(test_indent, 625, arg.client.address)
    c.line(test_indent, 623, 400, 623)
    c.drawString(test_indent, 605, arg.client.id_card if arg.client.id_card is not None else "")
    c.line(test_indent, 603, 400, 603)
    c.drawString(test_indent, 585, '+48 ' + arg.client.phone_number)
    c.line(test_indent, 583, 400, 583)
    c.setLineWidth(2)

    # ---------------------------------------------------

    c.drawString(40, 550, 'Wypożyczeniu podlega/podlegają:')
    c.line(35, 545, 230, 545)
    current_height = 525
    for equipment in arg.equipments:
        c.drawString(45, current_height, '• ' + equipment.name + ' (' + str(equipment.price) + ' zł)')
        current_height -= 20

    # ---------------------------------------------------

    data = [['Miesięczny koszt wypożyczenia', '', str(sum(x.price for x in arg.equipments)) + ' zł'],
            ['Transport', 'do klienta', str(arg.transport_to) + ' zł'],
            ['', 'od klienta', str(arg.transport_from) + ' zł' if arg.transport_from is not None else ""],
            ['Kaucja', '', str(arg.deposit) + ' zł'],
            ['Suma', '', str(get_agreement_cost(arg)) + ' zł']]
    table_style = TableStyle()
    table_style.add('FONTNAME', (0, 0), (-1, -1), 'RobotoRegular')
    table_style.add('FONTSIZE', (0, 0), (-1, -1), 12)
    add_table_lines(table_style)
    set_table_spans(table_style)
    set_table_alignments(table_style)
    set_table_paddings(table_style)

    width = 300
    height = 300
    x = 40
    y = current_height - 120
    f = Table(data)
    f.setStyle(table_style)
    f.wrapOn(c, width, height)
    f.drawOn(c, x, y)

    # ---------------------------------------------------

    data = [['Orkres/y wypożyczenia', '', ''],
            ['Od', 'Do', 'Zapłacono']]
    add_payments_to_table(arg, data)
    table_style = TableStyle()
    table_style.add('FONTNAME', (0, 0), (-1, -1), 'RobotoRegular')
    table_style.add('FONTSIZE', (0, 0), (-1, -1), 12)
    add_table_lines(table_style)
    table_style.add('SPAN', (0, 0), (2, 0))
    table_style.add('VALIGN', (0, 0), (-1, -1), 'MIDDLE')
    table_style.add('ALIGN', (0, 0), (-1, -1), 'CENTER')
    table_style.add('LEFTPADDING', (0, 0), (-1, -1), 12)
    table_style.add('RIGHTPADDING', (0, 0), (-1, -1), 12)
    table_style.add('TOPPADDING', (0, 0), (-1, -1), 5)
    table_style.add('BOTTOMPADDING', (0, 0), (-1, -1), 5)

    width = 400
    height = 300
    x = 300
    y = current_height - 54 - len(arg.payments) * 22 - 44
    f = Table(data)

    f.setStyle(table_style)
    f.wrapOn(c, width, height)
    f.drawOn(c, x, y)

    # ---------------------------------------------------

    signature_height = current_height - 54 - len(arg.payments) * 22 - 30 - 44
    c.drawString(300, signature_height, 'Podpis wypożyczającego:')
    c.setLineWidth(1)
    c.line(pdfmetrics.stringWidth('Podpis wypożyczającego:', "RobotoRegular", 12) + 303, signature_height - 2, 550,
           signature_height - 2)
    c.setLineWidth(2)

    if signature_height < 290:
        c.showPage()
        signature_height = 790
        c.setFont("RobotoRegular", 12)
        c.setLineWidth(2)

    # ---------------------------------------------------

    payment_options_height = signature_height - 40
    c.drawString(40, payment_options_height, 'Opłaty za wypożyczenia sprzętu można dokonywać:')
    c.drawString(60, payment_options_height - 20,
                 '• Przelewem na konto bankowe, nr. konta: 12 1050 1924 1000 0090 7554 3786')
    c.drawString(60, payment_options_height - 40, '• Gotówką w sklepie medycznym Janex Jakub Jankowski:')
    c.drawString(80, payment_options_height - 60, '- 3 Maja 39 05-800 Pruszków')
    c.drawString(80, payment_options_height - 80, '- Marymoncka 105/7 01-802 Warszawa')
    c.drawString(40, payment_options_height - 110,
                 'Przy zwrocie sprzętu bądź przedłużeniu okresu wypożyczenia należy mieć przy sobie')
    c.drawString(40, payment_options_height - 125, 'umowę wypożyczenia.')

    # ---------------------------------------------------

    return_height = payment_options_height - 160
    c.setFont("RobotoBold", 14)
    c.drawCentredString(300, return_height, 'Zwrot')
    c.setFont("RobotoRegular", 12)
    c.drawString(40, return_height - 40, 'Data zwrotu:')
    c.setLineWidth(1)
    c.line(pdfmetrics.stringWidth('Data zwrotu:', "RobotoRegular", 12) + 43, return_height - 42, 220,
           return_height - 42)
    c.drawString(250, return_height - 40, 'Podpis pracownika sklepu:')
    c.line(pdfmetrics.stringWidth('Podpis pracownika sklepu:', "RobotoRegular", 12) + 253, return_height - 42, 505,
           return_height - 42)
    c.setLineWidth(2)

    # ---------------------------------------------------

    c.save()
    return file_path


def add_table_lines(table_style):
    table_style.add('LINEABOVE', (0, 0), (-1, -1), 1, colors.black)
    table_style.add('LINEBELOW', (0, -1), (-1, -1), 1, colors.black)
    table_style.add('LINEBEFORE', (0, 0), (-1, -1), 1, colors.black)
    table_style.add('LINEAFTER', (-1, 0), (-1, -1), 1, colors.black)


def set_table_spans(table_style):
    table_style.add('SPAN', (0, 0), (1, 0))
    table_style.add('SPAN', (0, 1), (0, 2))
    table_style.add('SPAN', (0, 3), (1, 3))
    table_style.add('SPAN', (0, 4), (1, 4))


def set_table_alignments(table_style):
    table_style.add('ALIGN', (0, 0), (0, 0), 'RIGHT')
    table_style.add('ALIGN', (0, 0), (0, 0), 'RIGHT')
    table_style.add('VALIGN', (0, 0), (-1, -1), 'MIDDLE')
    table_style.add('ALIGN', (0, 0), (-1, -1), 'RIGHT')
    table_style.add('ALIGN', (0, 1), (0, 1), 'CENTER')  # Transport alignment


def set_table_paddings(table_style):
    table_style.add('RIGHTPADDING', (0, 0), (-1, -1), 5)
    table_style.add('TOPPADDING', (0, 0), (-1, -1), 5)
    table_style.add('BOTTOMPADDING', (0, 0), (-1, -1), 5)
    table_style.add('RIGHTPADDING', (0, 1), (0, 1), 15)


def get_agreement_cost(agreement: Agreement):
    result = 0
    result += sum(x.price for x in agreement.equipments)
    result += agreement.transport_to
    result += agreement.deposit
    if agreement.transport_from is not None:
        result += agreement.transport_from
    return result


def add_payments_to_table(agreement: Agreement, table):
    for payment in agreement.payments:
        table.append([payment.start.strftime("%d.%m.%Y"), payment.end.strftime("%d.%m.%Y"), str(payment.value) + ' zł'])
    table.append(['', '', ''])
    table.append(['', '', ''])
