# Ebook.Commerce





\## Project Overview



Ebook.Commerce is a modern e‑commerce web application built with \*\*ASP.NET Core MVC\*\*. It provides a full‑featured online store for digital books, supporting both \*\*admin\*\* (store management) and \*\*customer\*\* (shopping) experiences. The solution follows a clean \*\*4‑layer architecture\*\* (Presentation → Application → Domain → Infrastructure) to keep the codebase maintainable and scalable.



\---



\## 🎯 Technologies Used



| Category | Technology |

|---|---|

| \*\*Framework\*\* | ASP.NET Core MVC (C#) |

| \*\*ORM\*\* | Entity Framework Core |

| \*\*Database\*\* | Microsoft SQL Server |

| \*\*Front‑end\*\* | HTML5, CSS3, JavaScript (ES6+), Razor Views |

| \*\*Styling\*\* | Bootstrap 5 (responsive layout) |

| \*\*Dependency Injection\*\* | Built‑in ASP.NET Core DI |

| \*\*Authentication\*\* | ASP.NET Identity |

| \*\*Testing\*\* | xUnit, Moq |

| \*\*Build / Run\*\* | .NET 7 SDK, `dotnet` CLI |

| \*\*Version Control\*\* | Git |



\---



\## ⚙️ Installation \& Setup



1\. \*\*Prerequisites\*\*

&#x20;  - Windows 10/11 (or any OS with .NET SDK support)

&#x20;  - .NET 7 SDK installed (\[download](https://dotnet.microsoft.com/download))

&#x20;  - SQL Server instance (LocalDB works for development)



2\. \*\*Clone the repository\*\*

&#x20;  ```bash

&#x20;  git clone https://github.com/your-org/Ebook.Commerce.git

&#x20;  cd Ebook.Commerce

&#x20;  ```



3\. \*\*Configure the database\*\*

&#x20;  - Open `appsettings.json` and update the `ConnectionStrings:DefaultConnection` to point to your SQL Server.

&#x20;  - Run the migrations to create the schema:

&#x20;    ```bash

&#x20;    dotnet ef database update

&#x20;    ```



4\. \*\*Run the application\*\*

&#x20;  ```bash

&#x20;  dotnet run --project Ebook.Commerce/Ebook.Web

&#x20;  ```

&#x20;  The site will be available at `https://localhost:5001`.



\---



\## 📖 User Manual



\### 🎚️ Admin Interface (Store Management)

\- \*\*Login\*\*: Access the admin area via `/Admin/Account/Login`.

\- \*\*Dashboard\*\*: Overview of orders, revenue, and inventory.

\- \*\*Product Management\*\*:

&#x20; - List, create, edit, and delete books.

&#x20; - Upload cover images and set pricing.

\- \*\*Category Management\*\*:

&#x20; - Organize books into hierarchical categories.

\- \*\*Order Processing\*\*:

&#x20; - View order details, update status (Processing, Shipped, Completed, Cancelled).

\- \*\*User Management\*\*:

&#x20; - Manage customer accounts and roles.



> \*\*Tip:\*\* All admin pages use a clean, responsive Bootstrap layout with intuitive icons and tooltips for a premium feel.



\### 🛒 Customer Interface (Shopping Experience)

\- \*\*Browse Catalog\*\*: Navigate categories or search by title/author.

\- \*\*Product Details\*\*: View description, sample pages, and price.

\- \*\*Cart\*\*:

&#x20; - Add, remove, and change quantity of items.

&#x20; - Cart persists across sessions for logged‑in users.

\- \*\*Checkout\*\*:

&#x20; - Fill shipping information, choose payment method, and place order.

&#x20; - Order confirmation email is sent via the built‑in email service.

\- \*\*Account Dashboard\*\*:

&#x20; - View order history, download purchased e‑books, and edit profile.



> \*\*User‑friendly design:\*\* The UI employs a modern dark‑mode compatible theme, smooth hover animations, and clear CTA buttons to guide users effortlessly.



\---



\## 🛠️ Development Guidelines



\- Follow the \*\*clean architecture\*\* layers: Controllers → Services → Repositories → DbContext.

\- Write unit tests for service methods using \*\*xUnit\*\* and \*\*Moq\*\*.

\- Use \*\*dependency injection\*\* to keep components loosely coupled.

\- Run `dotnet format` and `dotnet build` before committing.



\---



\## 🤝 Contributing



1\. Fork the repository.

2\. Create a feature branch (`git checkout -b feature/YourFeature`).

3\. Commit your changes with clear messages.

4\. Open a Pull Request targeting the `main` branch.



Please adhere to the existing coding style and include unit tests for new functionality.



\---



\## 📄 License



This project is licensed under the \*\*MIT License\*\* – see the `LICENSE` file for details.



\---



\*Created by the Ebook.Commerce team – delivering a premium, user‑friendly digital book store.\*



