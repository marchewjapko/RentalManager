from dotenv import load_dotenv
from fastapi import FastAPI
from fastapi.openapi.utils import get_openapi
from starlette.responses import FileResponse
from CreatePDF import generate_pdf
from fastapi.middleware.cors import CORSMiddleware
from Models import Agreement
from urllib.parse import quote
import os
import json

app = FastAPI()
load_dotenv()

origins = ["*"]
app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.post("/documents/generate_document")
async def generate_document(arg: Agreement):
    file_path = generate_pdf(arg)
    response = FileResponse(file_path, 200)
    response.headers["Content-Disposition"] = "attachment; filename*=utf-8''{}.pdf".format(
        quote('Umowa wypożyczenia - ' + arg.client.name + ' ' + arg.client.surname))
    return response


if os.getenv('ENVIRONMENT') == 'Development':
    with open('../RentalManager.WebApp/src/references/documentServiceAPI.json', 'w') as f:
        json.dump(get_openapi(
            title=app.title,
            version=app.version,
            openapi_version=app.openapi_version,
            description=app.description,
            routes=app.routes
        ), f)
