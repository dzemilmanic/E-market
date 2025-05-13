# 🛒 E-Commerce WPF Application

This is a desktop-based **E-commerce platform** developed using **C# and WPF** with **SQL Server** as the database. It supports two types of users: **Customers** and **Sellers (Companies)**.

---

## 🔐 Login System

Upon launching the application, users are prompted to **log in** with their credentials. Based on their role, different features and interfaces are made available.

- 👤 **Customer**  
- 🏢 **Seller (Company)**

---

## 👥 User Roles & Features

### 🧍‍♂️ Customer

- 🛍️ **Create Orders**: Add products to a cart and place an order.
- 📜 **View Orders**: Review a list of all their orders with current status.
- ✉️ **Messaging**: Send and receive messages from the seller.
- 📧 **Email Support**: Contact seller via email.
- 📊 **Spending Report**: View how much they have spent. Can generate report as a `.txt` file.

### 🏪 Seller (Company)

- ➕ **Add Products**: Add new products to their inventory.
- 📝 **Edit Products**: Update existing product details.
- ❌ **Delete Products**: Remove products from inventory.
- 📦 **Manage Orders**: View and update the status of all received orders.
- ✉️ **Messaging**: Communicate with customers via in-app messages.
- 📧 **Email Customers**: Contact buyers via email.
- 💰 **Earnings Report**: Track total revenue and export report to `.txt` file.

---

## 🧱 Technologies Used

- **Frontend**: C# with WPF (Windows Presentation Foundation)
- **Authentication**: Role-based login (Customer / Seller)
- **Messaging & Email**: In-app messaging + SMTP email support
- **Reporting**: Generates summary reports (text file export)

---

## 🚀 Getting Started

1. Clone the repository.
2. Set up the SQL Server database using the provided scripts.
3. Adjust the connection string in `App.config`.
4. Run the application in Visual Studio.

---

## 📝 Future Improvements

- PDF/Excel export for reports  
- Multi-language support  
- Product image uploads  
- Advanced analytics dashboard  

---

## 🪪 License

This project is licensed under the **MIT License**.

---

## 📽️ Demo Video

📺 [Watch the Demo](https://presentation-wpf-project.netlify.app/)

---

Happy coding! 💻
