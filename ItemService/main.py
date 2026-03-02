from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List

app = FastAPI()

# In-memory storage
items = ["Book", "Laptop", "Phone"]

class Item(BaseModel):
    name: str

# GET /items
@app.get("/items")
def get_items():
    return items

# POST /items
@app.post("/items", status_code=201)
def add_item(item: Item):
    items.append(item.name)
    return {"message": f"Item added: {item.name}"}

# GET /items/{id}
@app.get("/items/{id}")
def get_item(id: int):
    if id < 0 or id >= len(items):
        raise HTTPException(status_code=404, detail="Item not found")
    return items[id]