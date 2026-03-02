from django.urls import path
from . import views

urlpatterns = [
    path('', views.get_orders),
    path('<int:id>', views.get_order_by_id),
    path('create', views.create_order),
]