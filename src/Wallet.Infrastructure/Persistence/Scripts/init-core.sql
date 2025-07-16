-- Índice
CREATE INDEX IX_Accounts_ClientId ON Accounts(ClientId);

-- Dados fictícios
-- Cliente 1
INSERT INTO Clients (Id, Name, Email)
VALUES
  ('11111111-1111-1111-1111-111111111111', 'João Otavio', 'joao.otavio@example.com');

-- Contas para o Cliente 1
INSERT INTO Accounts (Id, ClientId, Balance, CreatedAt, UpdateAt)
VALUES
  ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '11111111-1111-1111-1111-111111111111', 1500.00, NOW(6), NOW(6)),
  ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '11111111-1111-1111-1111-111111111111', 320.50, NOW(6), NOW(6));

-- Cliente 2
INSERT INTO Clients (Id, Name, Email)
VALUES
  ('22222222-2222-2222-2222-222222222222', 'Maria Silva', 'maria.silva@example.com');

-- Contas para o Cliente 2
INSERT INTO Accounts (Id, ClientId, Balance, CreatedAt, UpdateAt)
VALUES
  ('cccccccc-cccc-cccc-cccc-cccccccccccc', '22222222-2222-2222-2222-222222222222', 8200.75, NOW(6), NOW(6));
