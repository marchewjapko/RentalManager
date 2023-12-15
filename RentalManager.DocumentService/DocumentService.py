from fastapi import FastAPI
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
    response.headers["Content-Disposition"] = "attachment; filename=Attachment.pdf"
    return response