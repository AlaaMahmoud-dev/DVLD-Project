<h1 align="center">🚗 Drivers & Vehicles License Department System (DVLD)</h1>
<h3 align="center">A Desktop Application simulating a real-world licensing system</h3>

---

## 📌 Overview
The **DVLD System** is a desktop application built using **.NET (WinForms)** that simulates a simplified real-world Driver and Vehicle Licensing Department system.

The project focuses on managing driving license applications, tracking their status, and handling the required testing process before issuing a license.
---

## 🚀 Key Features

### 📝 License Application Management
- Apply for:
  - Local Driving License (Classes 1–7)
  - International Driving License
- Prevent duplicate active applications for the same person
- Full tracking of application status

---

### 🧪 Testing Process Workflow
Applicants must pass **3 sequential tests**:

1. 👁️ Vision Test  
2. 📝 Written Test  
3. 🚗 Street Test  

⚠️ Rules enforced:
- Cannot proceed to the next test without passing the previous one
- Failed tests require a **retake test application**
- Each test has scheduled appointments and status tracking

---

### 🪪 License Management
- Issue license after passing all tests
- Renew expired licenses
- Replace lost or damaged licenses

---

### 👥 People & User Management
- Manage general person data
- Manage system users
- Manage drivers data

---

## 🧠 System Workflow (Use Case)

1. Add a new person (applicant)
2. Create a **Local License Application**
3. Select license class (1–7)
4. Schedule and pass:
   - Vision Test → Written Test → Street Test
5. Issue license after passing all tests

---

## 🏗️ Architecture

The system is built using **3-Tier Architecture**:

- 🎨 Presentation Layer (WinForms UI)
- ⚙️ Business Logic Layer (Validation & Rules)
- 🗄️ Data Access Layer (Database Communication using ADO.NET)

### ✅ Benefits:
- Better **code organization**
- Improved **maintainability & scalability**
- Clear separation of concerns
- Easier debugging and future enhancements

---

## 🛠️ Technologies Used

- **C# / .NET (WinForms)**
- **SQL Server**
- **ADO.NET**
- **3-Tier Architecture**

---

## 🗄️ Database Design

### Core Tables:
- `Applications`
- `LocalDrivingLicenseApplications`
- `LicenseClasses` (Lookup)
- `TestAppointments`
- `TestTypes` (Lookup)
- `Licenses`
- `DetainedLicenses`

---

## ⚠️ Business Rules & Validations

The system enforces real-world constraints:

- ❌ Cannot apply for a new license while an active application exists  
- ❌ Cannot issue a license before passing all required tests  
- ❌ Cannot renew a license that is not expired  
- ❌ Cannot schedule a test:
  - If an active appointment already exists  
  - If the previous test is not passed  
  - If the test is already passed  

---
## 📸 System Screenshots

### 🔐 Login System
Secure login interface for system users.
<p align="center">
  <img src="https://github.com/user-attachments/assets/dc98e115-6af2-44e8-af45-f92a2521fef4" width="70%">
</p>

---

### 📝 License Application Dashboard
Main interface to manage and track license applications.
<p align="center">
  <img src="https://github.com/user-attachments/assets/4a5fc61b-353e-4c73-9e8a-bfa6977ada4b" width="85%">
</p>

---

### ➕ Creating a New Application
Adding a new local driving license request with full applicant details.
<p align="center">
  <img src="https://github.com/user-attachments/assets/8b497cc2-2b7f-4d81-b768-bde457a6f369" width="85%">
  <br/><br/>
  <img src="https://github.com/user-attachments/assets/73adc4fd-cff7-4bdc-a125-698d966a3a8c" width="85%">
</p>

---

### 📄 Application Details
View full application information and current status.
<p align="center">
  <img src="https://github.com/user-attachments/assets/ffa9e4ae-71fb-44a9-b66f-65f4b87ad54a" width="85%">
</p>

---

### 🧪 Test Scheduling Workflow
Managing the 3-step testing process (Vision → Written → Street).
<p align="center">
  <img src="https://github.com/user-attachments/assets/f0e910d5-4779-4989-9e05-1ffc24d2c5f4" width="85%">
  <br/><br/>
  <img src="https://github.com/user-attachments/assets/6bb7c084-618f-4788-bc6a-abf8d87b8053" width="85%">
  <br/><br/>
  <img src="https://github.com/user-attachments/assets/4b5e5929-5406-48b2-8e01-254e1e172fdf" width="85%">
</p>

---

### 🔁 Test Retake Handling
Handling failed tests with re-application and tracking.
<p align="center">
  <img src="https://github.com/user-attachments/assets/a009a9a0-ca00-4e18-bb11-f275100cc8ae" width="85%">
</p>

---

### 🪪 License Issuing Process
Final step after passing all tests.
<p align="center">
  <img src="https://github.com/user-attachments/assets/b0272da3-dd49-45c0-8d70-2c2a35840999" width="85%">
  <br/><br/>
  <img src="https://github.com/user-attachments/assets/dd5e027f-c89d-4317-8826-384312507764" width="85%">
</p>

---

### 📇 License Information
Displaying issued license details.
<p align="center">
  <img src="https://github.com/user-attachments/assets/47397134-dec3-44a8-98ce-4a850d7b2601" width="85%">
</p>

---
## ⭐ What Makes This Project Stand Out

✔️ Real-world system simulation with complex workflows (beyond basic CRUD operations)  
✔️ End-to-end process: application → multi-stage testing → license issuing  
✔️ Robust implementation of **3-Tier Architecture** ensuring scalability and separation of concerns  
✔️ Strict enforcement of **business rules and validation logic** reflecting real-world constraints  

✔️ Designed a modular **Presentation Layer (WinForms)** using reusable **User Controls**  
✔️ Reduced UI duplication and improved maintainability through component-based design  
✔️ Ensured consistent user experience across the system by standardizing UI elements  
✔️ Enhanced code readability and testability by isolating UI logic from business logic  

✔️ Database-driven architecture with structured relationships and lookup tables  
✔️ Focus on building a **maintainable, extensible, and production-like system**

---

## 📌 Future Improvements
- Add reporting & analytics
- Enhance UI/UX design
- Migrate to Web API + Frontend (Full Stack upgrade)

---

## 📫 Author
👤 Ala'a  
🔗 https://github.com/AlaaMahmoud-dev
