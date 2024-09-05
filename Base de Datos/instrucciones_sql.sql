
-- Create para tabla users
CREATE TABLE Users (
    id SERIAL PRIMARY KEY,
    nombres VARCHAR(255) NOT NULL,
    apellidos VARCHAR(255) NOT NULL,
    alias VARCHAR(50) NOT NULL,
    fechanacimiento DATE NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    password VARCHAR(120) NOT NULL,
    telefono VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    fechacreacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fechamodificacion TIMESTAMP
);

-- Inserts para datos de pruebas
INSERT INTO Users (nombres, apellidos, fechanacimiento, direccion, password, telefono, email, fechacreacion) VALUES
('Juan', 'Pérez', '1990-05-15', '123 Calle Sol', 'pass1234', '555-0101', 'juan.perez@example.com', CURRENT_TIMESTAMP),
('Maria', 'Lopez', '1985-07-20', '456 Calle Luna', 'pass5678', '555-0202', 'maria.lopez@example.com', CURRENT_TIMESTAMP),
('Carlos', 'Garcia', '1975-09-25', '789 Calle Estrella', 'pass91011', '555-0303', 'carlos.garcia@example.com', CURRENT_TIMESTAMP),
('Ana', 'Martinez', '1988-12-30', '321 Calle Cometa', 'pass1213', '555-0404', 'ana.martinez@example.com', CURRENT_TIMESTAMP),
('Pedro', 'Alvarez', '1992-03-22', '654 Calle Planeta', 'pass1415', '555-0505', 'pedro.alvarez@example.com', CURRENT_TIMESTAMP),
('Lucía', 'Fernandez', '1993-08-11', '987 Calle Galaxia', 'pass1617', '555-0606', 'lucia.fernandez@example.com', CURRENT_TIMESTAMP),
('Roberto', 'Vargas', '1980-01-15', '246 Calle Nebulosa', 'pass1819', '555-0707', 'roberto.vargas@example.com', CURRENT_TIMESTAMP),
('Daniela', 'Mora', '1979-04-10', '135 Calle Meteorito', 'pass2021', '555-0808', 'daniela.mora@example.com', CURRENT_TIMESTAMP),
('Tomás', 'Rivera', '1986-06-17', '864 Calle Eclipse', 'pass2223', '555-0909', 'tomas.rivera@example.com', CURRENT_TIMESTAMP),
('Isabel', 'Quintana', '1991-11-19', '975 Calle Cosmos', 'pass2425', '555-1010', 'isabel.quintana@example.com', CURRENT_TIMESTAMP);


-- funcion para actualizar fecha de modificacion en conjunto con trigger
CREATE OR REPLACE FUNCTION update_fecha_modificacion()
RETURNS TRIGGER AS $$
BEGIN
    NEW.fechamodificacion = now();  -- Establece la columna 'fechamodificacion' al tiempo actual
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_user_modification_time
BEFORE UPDATE ON Users
FOR EACH ROW
EXECUTE FUNCTION update_fecha_modificacion();
