from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework import status

orders = []
id_counter = 1

@api_view(['GET'])
def get_orders(request):
    return Response(orders)

@api_view(['POST'])
def create_order(request):
    global id_counter

    order = request.data
    order['id'] = id_counter
    order['status'] = "PENDING"

    orders.append(order)
    id_counter += 1

    return Response(order, status=status.HTTP_201_CREATED)

@api_view(['GET'])
def get_order_by_id(request, id):
    for order in orders:
        if order['id'] == id:
            return Response(order)

    return Response({"error": "Not found"}, status=404)