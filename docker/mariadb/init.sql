CREATE TABLE staff (
    staff_id VARCHAR(8) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    birthday DATE,
    gender VARCHAR(10),
    PRIMARY KEY (staff_id)
);

ALTER TABLE staff ADD CONSTRAINT chk_gender CHECK (gender IN ('male', 'female') OR gender IS NULL);

CREATE INDEX idx_gender ON staff (gender);

CREATE INDEX idx_birthday ON staff (birthday);
