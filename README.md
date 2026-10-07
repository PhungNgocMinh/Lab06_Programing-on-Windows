# ProductManager

A WinForms (C#) desktop application for managing a product inventory, built as a lab project for the Windows Programming course. This repository covers the topic: **Data Binding & Advanced DataGridView**.

## Overview

ProductManager lets users manage a list of products stored in memory. Beyond the basic CRUD features, this project focuses on how data binding works in WinForms and on getting the most out of `DataGridView`: clean formatting, strict selection behavior, and click-to-sort columns.

## Features

### Core features
- **Product model**: `Product` class with `MaSP`, `TenSP`, `LoaiSP`, `DonGia`, `SoLuong`, `NgayNhap`, `ConKinhDoanh`, `DuongDanAnh`
- **In-memory storage** using `BindingList<Product>` bound through a `BindingSource`
- **Sample data**: at least 3 products loaded when the form opens
- **Basic operations**: Add, Edit, Delete, Search, and Refresh

### Advanced features
- **DataGridView configuration**
  - `ReadOnly = true` to prevent direct editing in the grid
  - `SelectionMode = FullRowSelect` to select an entire row
  - `MultiSelect = false` to allow only one row at a time
- **Column formatting**
  - `DonGia` shown as currency with thousand separators (e.g. `1,500,000 VNĐ`)
  - `NgayNhap` shown as `dd/MM/yyyy`
- **Column sorting**: click the header of `DonGia` or `SoLuong` to sort the data

## Topics Studied

1. **`List<T>` vs `BindingList<T>` vs `BindingSource`**: how they differ and when to use each
2. **Why the UI refreshes automatically** when the underlying data changes (change notifications via `IBindingList` / `ListChanged`)
3. **The role of `BindingSource`** as a mediator between data and controls
4. **How the sorting logic works**, with a code walkthrough

## Tech Stack

| Item | Details |
|------|---------|
| Language | C# |
| Framework | .NET (Windows Forms) |
| IDE | Visual Studio |
| Storage | In-memory (no database) |

## Getting Started

```bash
git clone https://github.com/PhungNgocMinh/Lab06_programing-on-windows.git
cd ProductManager
```

1. Open `ProductManager.sln` in Visual Studio
2. Build the solution (`Ctrl + Shift + B`)
3. Run the app (`F5`)

## Project Structure

```
ProductManager/
├── Models/
│   └── Product.cs          # Product class
├── Forms/
│   └── MainForm.cs         # Main UI and event handling
├── Program.cs
└── README.md
```

## Screenshots

(None yet)

## Authors

(None yet)

## Course Information

- **Course**: Programing on Windows
- **Instructor**: (None yet)
- **Academic year**: 2026-2027
