from datetime import datetime
from pydantic import BaseModel
from pydantic.types import List, Optional


class Equipment(BaseModel):
    name: str
    price: int


class Client(BaseModel):
    name: str
    surname: str
    address: str
    phone_number: str
    id_card: Optional[str]


class Payment(BaseModel):
    start: datetime
    end: datetime
    value: int


class Agreement(BaseModel):
    client: Client
    equipments: List[Equipment]
    payments: List[Payment]
    transport_from: Optional[int]
    transport_to: int
    deposit: int
