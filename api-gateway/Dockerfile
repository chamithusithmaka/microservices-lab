# Use official Node.js Alpine image for minimal size
FROM node:20-alpine

# Set working directory
WORKDIR /app

# Install dependencies first (for better layer caching)
COPY package*.json ./
RUN npm install --production

# Copy the rest of the code
COPY . .

# Expose API Gateway port
EXPOSE 8080

# Start the API Gateway
CMD ["node", "index.js"]
