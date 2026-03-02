const express = require('express');
const { createProxyMiddleware } = require('http-proxy-middleware');

const app = express();
const PORT = 8080;

// JSON parser for POST requests
app.use(express.json());

// Proxy rules
app.use('/items', createProxyMiddleware({
  target: 'http://item-service:8081',
  changeOrigin: true,
  pathRewrite: {
    '^/items': '', // remove /items prefix when forwarding
  },
}));

app.use('/orders', createProxyMiddleware({
  target: 'http://order-service:8082',
  changeOrigin: true,
  pathRewrite: {
    '^/orders': '', // remove /orders prefix
  },
}));

app.use('/payments', createProxyMiddleware({
  target: 'http://payment-service:8083',
  changeOrigin: true,
  pathRewrite: {
    '^/payments': '', // remove /payments prefix
  },
}));

app.listen(PORT, () => {
  console.log(`API Gateway running on port ${PORT}`);
});