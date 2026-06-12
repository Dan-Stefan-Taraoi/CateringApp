-- =============================================
-- CateringApp Seed Data
-- =============================================

-- Categories (already seeded via EF, but included for completeness)
-- 1 = Ingredients, 2 = Equipment, 3 = Maintenance, 4 = Operational

-- =============================================
-- CLIENTS
-- =============================================
INSERT INTO Clients (Name) VALUES
('Table_05'),
('Table_12'),
('Table_18'),
('Firma Mueller GmbH'),
('EventPro Nürnberg');

-- =============================================
-- ITEMS (Inventory)
-- =============================================

-- Ingredients (CategoryId = 1)
INSERT INTO Items (Name, Price, Quantity, Unit, IsAvailable, CategoryId) VALUES
('Flour',           0.80,  50.0,  'kg',   1, 1),
('Olive Oil',       3.50,  20.0,  'L',    1, 1),
('Tomato Sauce',    1.20,  30.0,  'L',    1, 1),
('Mozzarella',      4.50,  15.0,  'kg',   1, 1),
('Guanciale',       8.00,  10.0,  'kg',   1, 1),
('Eggs',            0.30,  100.0, 'pcs',  1, 1),
('Pecorino',        6.50,  8.0,   'kg',   1, 1),
('Spinach',         1.80,  12.0,  'kg',   1, 1),
('Ricotta',         3.20,  10.0,  'kg',   1, 1),
('Black Pepper',    0.50,  5.0,   'kg',   1, 1),
('Pasta Sheets',    2.00,  20.0,  'kg',   1, 1),
('Beef Mince',      7.50,  15.0,  'kg',   1, 1),
('Béchamel',        1.50,  10.0,  'L',    1, 1),
('Mushrooms',       2.80,  8.0,   'kg',   1, 1),
('Garlic',          0.40,  5.0,   'kg',   1, 1);

-- Equipment (CategoryId = 2)
INSERT INTO Items (Name, Price, Quantity, Unit, IsAvailable, CategoryId) VALUES
('Oven',            1200.00, 2.0, 'pcs',  1, 2),
('Fryer',           850.00,  1.0, 'pcs',  1, 2),
('Mixing Bowl',     15.00,   10.0,'pcs',  1, 2),
('Chef Knife Set',  120.00,  5.0, 'pcs',  1, 2),
('Cutting Board',   25.00,   8.0, 'pcs',  1, 2);

-- Maintenance (CategoryId = 3)
INSERT INTO Items (Name, Price, Quantity, Unit, IsAvailable, CategoryId) VALUES
('Cleaning Spray',  3.50,  20.0, 'pcs',  1, 3),
('Dish Soap',       2.00,  30.0, 'L',    1, 3),
('Oven Cleaner',    6.00,  10.0, 'pcs',  1, 3),
('Gloves',          0.50,  100.0,'pcs',  1, 3),
('Trash Bags',      0.20,  200.0,'pcs',  1, 3);

-- Operational (CategoryId = 4)
INSERT INTO Items (Name, Price, Quantity, Unit, IsAvailable, CategoryId) VALUES
('Chair',           45.00,  80.0, 'pcs',  1, 4),
('Folding Table',   90.00,  20.0, 'pcs',  1, 4),
('Tablecloth',      8.00,   50.0, 'pcs',  1, 4),
('Serving Tray',    12.00,  30.0, 'pcs',  1, 4),
('Chafing Dish',    35.00,  15.0, 'pcs',  1, 4);

-- =============================================
-- MENU ITEMS
-- =============================================
-- CookingMethod: 0 = Fried, 1 = Baked, 2 = Cold
INSERT INTO MenuItems (Name, Description, Price, CookingMethod, Serves) VALUES
('Pizza Margherita',    'Classic tomato and mozzarella pizza',              12.00, 1, 2),
('Pizza Carbonara',     'Pizza with guanciale, eggs and pecorino',          15.00, 1, 2),
('Pasta Carbonara',     'Pasta with guanciale, eggs, pecorino and pepper',  13.50, 2, 1),
('Lasagne Bolognese',   'Classic beef and béchamel lasagne',                14.00, 1, 4),
('Lasagne Florentine',  'Spinach and ricotta lasagne',                      13.00, 1, 4),
('Spinach Cannelloni',  'Cannelloni filled with spinach and ricotta',       12.50, 1, 3),
('Mushroom Risotto',    'Creamy risotto with mixed mushrooms and garlic',   11.00, 2, 2),
('Fried Calamari',      'Crispy fried calamari with lemon',                 10.00, 0, 2),
('Caprese Salad',       'Fresh mozzarella, tomato and basil',               8.50,  2, 2),
('Tiramisu',            'Classic Italian dessert with mascarpone',          6.50,  2, 4);

-- =============================================
-- KITCHEN ITEMS (MenuItem <-> Item links)
-- Get IDs first with SELECT, then adjust if needed
-- =============================================

-- Pizza Margherita (Id 1) needs: Flour, Tomato Sauce, Mozzarella, Olive Oil
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (1, 1), (1, 3), (1, 4), (1, 2);

-- Pizza Carbonara (Id 2) needs: Flour, Guanciale, Eggs, Pecorino, Black Pepper
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (2, 1), (2, 5), (2, 6), (2, 7), (2, 10);

-- Pasta Carbonara (Id 3) needs: Eggs, Guanciale, Pecorino, Black Pepper
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (3, 6), (3, 5), (3, 7), (3, 10);

-- Lasagne Bolognese (Id 4) needs: Pasta Sheets, Beef Mince, Béchamel, Tomato Sauce
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (4, 11), (4, 12), (4, 13), (4, 3);

-- Lasagne Florentine (Id 5) needs: Pasta Sheets, Spinach, Ricotta, Béchamel
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (5, 11), (5, 8), (5, 9), (5, 13);

-- Spinach Cannelloni (Id 6) needs: Pasta Sheets, Spinach, Ricotta, Tomato Sauce
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (6, 11), (6, 8), (6, 9), (6, 3);

-- Mushroom Risotto (Id 7) needs: Mushrooms, Garlic, Olive Oil
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (7, 14), (7, 15), (7, 2);

-- Fried Calamari (Id 8) needs: Flour, Olive Oil
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (8, 1), (8, 2);

-- Caprese Salad (Id 9) needs: Mozzarella, Tomato Sauce, Olive Oil
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (9, 4), (9, 3), (9, 2);

-- Tiramisu (Id 10) needs: Eggs, Pecorino (placeholder — add Mascarpone to Items if needed)
INSERT INTO KitchenItems (MenuItemId, ItemId) VALUES (10, 6), (10, 7);