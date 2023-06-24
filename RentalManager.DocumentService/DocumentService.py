from fastapi import FastAPI
from fastapi.openapi.docs import get_swagger_ui_html
from starlette.responses import FileResponse

from CreatePDF import generate_pdf
from fastapi.middleware.cors import CORSMiddleware

from Models import Agreement

app = FastAPI()

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
    response.headers["Content-Disposition"] = 'attachment; filename=Agreement.pdf'
    return response


@app.get("/")
def read_docs():
    return get_swagger_ui_html(openapi_url="/openapi.json", title='test')
