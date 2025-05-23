# 🛒 Ecommerce API Documentation

The Ecommerce API is a backend system built to support the core functionality of an online shopping platform. It includes features for managing products, user authentication, shopping carts (baskets), customer orders, and more. This RESTful API is suitable for powering both web and mobile e-commerce applications.

---

## 📦 Overview of Core Modules

### 1. **Product Management**
The product module allows clients to retrieve product listings, view product details, filter by brand or type, search by name, sort by price, and handle pagination.

- **Listing**: Retrieve a list of available products.
- **Details**: Fetch individual product details using a unique identifier.
- **Sorting**: Products can be sorted in ascending or descending order by price.
- **Filtering**: Products can be filtered by brand or type.
- **Search**: Clients can search products by name.
- **Pagination**: Supports paginated responses for large product catalogs.
- **CRUD**: Create, update, or delete product entries.

### 2. **Product Categories**
Two main categorizations are used:

- **Brands**: Represents the brand or manufacturer of the product.
- **Types**: Represents the category or type of the product (e.g., electronics, clothing, etc.).

Clients can fetch the full list of available brands and types to populate dropdowns or filters.

---

### 3. **Basket (Shopping Cart)**
The basket module enables customers to add, view, and delete items in their shopping cart before placing an order.

- **Create**: Start a new basket.
- **Retrieve**: Fetch the contents of a customer's basket.
- **Delete**: Clear the basket or remove individual items.

---

### 4. **Order Management**
Once a basket is finalized, an order can be created.

- **Create Order**: Submit a new order with selected delivery options.
- **Order History**: Retrieve a list of previous orders for a user.
- **Order Details**: View the full detail of a specific order.
- **Delivery Methods**: List available shipping or delivery options.

---

### 5. **User Management**
The API supports basic authentication features and user information retrieval.

- **Registration**: New users can sign up.
- **Login**: Existing users can authenticate and receive a token.
- **Get Current User**: Retrieve information about the authenticated user.

---

### 6. **User Address**
Customers can store and update their delivery address.

- **Retrieve Address**: Get the saved shipping address.
- **Update Address**: Change or update the address for future deliveries.

---

## 🛠️ Developer Notes

- **Authentication**: Some endpoints require token-based authentication. Always include the `Authorization` header when accessing protected routes.
- **Pagination**: When working with product lists, use pagination parameters to avoid loading large datasets all at once.
- **Error Handling**: The API uses standard HTTP response codes and often includes helpful error messages in the response body.

---

## 🚧 Recommendations

- Update HTTP methods (e.g., avoid using GET for actions like adding orders or products).
- Use HTTPS in production for secure data transmission.
- Validate inputs on both client and server side.
- Use consistent naming conventions for endpoints and query parameters.

---

