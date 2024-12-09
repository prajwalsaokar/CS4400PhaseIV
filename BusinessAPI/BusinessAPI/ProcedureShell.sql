-- CS4400: Introduction to Database Systems (Fall 2024)
-- Project Phase III: Stored Procedures SHELL [v3] Thursday, Nov 7, 2024
set global transaction isolation level serializable;
set global SQL_MODE = 'ANSI,TRADITIONAL';
set names utf8mb4;
set SQL_SAFE_UPDATES = 0;

use business_supply;
-- -----------------------------------------------------------------------------
-- stored procedures and views
-- -----------------------------------------------------------------------------
/* Standard Procedure: If one or more of the necessary conditions for a procedure to
be executed is false, then simply have the procedure halt execution without changing
the database state. Do NOT display any error messages, etc. */

-- [1] add_owner()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new owner.  A new owner must have a unique
username. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_owner;
delimiter //
create procedure add_owner (in ip_username varchar(40), in ip_first_name varchar(100),
                            in ip_last_name varchar(100), in ip_address varchar(500), in ip_birthdate date)
    sp_main: begin
    -- ensure new owner has a unique username
    DECLARE username_count INT;
    -- Check if alr exists
SELECT COUNT(*) INTO username_count FROM users WHERE username = ip_username;

-- If the username doesn't exist, add the new owner
IF username_count = 0 THEN
        INSERT INTO users (username, first_name, last_name, address, birthdate)
        VALUES (ip_username, ip_first_name, ip_last_name, ip_address, ip_birthdate);
INSERT INTO business_owners (username)
VALUES (ip_username);
END IF;
end //
delimiter ;

-- [2] add_employee()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new employee without any designated driver or
worker roles.  A new employee must have a unique username and a unique tax identifier. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_employee;
delimiter //
create procedure add_employee (in ip_username varchar(40), in ip_first_name varchar(100),
                               in ip_last_name varchar(100), in ip_address varchar(500), in ip_birthdate date,
                               in ip_taxID varchar(40), in ip_hired date, in ip_employee_experience integer,
                               in ip_salary integer)
    sp_main: begin
    -- ensure new owner has a unique username
    -- ensure new employee has a unique tax identifier
    IF ip_username NOT IN (Select username from users) and ip_first_name NOT IN (Select id from delivery_services) THEN
    INSERT INTO users VALUES(ip_username, ip_first_name, ip_last_name, ip_address, ip_birthdate);
INSERT INTO employees VALUES(ip_username, ip_taxID, ip_hired, ip_employee_experience, ip_salary);
END IF;
end //
delimiter ;

-- [3] add_driver_role()
-- -----------------------------------------------------------------------------
/* This stored procedure adds the driver role to an existing employee.  The
employee/new driver must have a unique license identifier. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_driver_role;
delimiter //
create procedure add_driver_role (in ip_username varchar(40), in ip_licenseID varchar(40),
                                  in ip_license_type varchar(40), in ip_driver_experience integer)
    sp_main: begin
    -- ensure employee exists and is not a worker
    -- ensure new driver has a unique license identifier
    IF ip_username IN (select username from employees) and 
    ip_username NOT IN (select username from workers) and
    ip_license_type NOT IN (Select licenseID from drivers) THEN
    INSERT INTO drivers VALUES(ip_username, ip_licenseID, ip_license_type, ip_driver_experience);
END IF;
end //
delimiter ;

-- [4] add_worker_role()
-- -----------------------------------------------------------------------------
/* This stored procedure adds the worker role to an existing employee. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_worker_role;
delimiter //
create procedure add_worker_role (in ip_username varchar(40))
    sp_main: begin
    -- ensure employee exists and is not a driver
    IF EXISTS(Select 1 from employees where ip_username=username) and
    NOT EXISTS(Select 1 from drivers where ip_username=username)
    THEN
    INSERT INTO workers VALUES(ip_username);
END IF;
end //
delimiter ;

-- [5] add_product()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new product.  A new product must have a
unique barcode. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_product;
delimiter //
create procedure add_product (in ip_barcode varchar(40), in ip_name varchar(100),
                              in ip_weight integer)
    sp_main: begin
	-- ensure new product doesn't already exist
    DECLARE barcode_count INT;

    -- Check if the barcode already exists
SELECT COUNT(*) INTO barcode_count FROM products WHERE barcode = ip_barcode;

-- barcode doesn't exist, add the new product
IF barcode_count = 0 THEN
        INSERT INTO products (barcode, iname, weight)
        VALUES (ip_barcode, ip_name, ip_weight);
END IF;
end //
delimiter ;

-- [6] add_van()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new van.  A new van must be assigned 
to a valid delivery service and must have a unique tag.  Also, it must be driven
by a valid driver initially (i.e., driver works for the same service). And the van's starting
location will always be the delivery service's home base by default. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_van;
delimiter //
create procedure add_van (in ip_id varchar(40), in ip_tag integer, in ip_fuel integer,
                          in ip_capacity integer, in ip_sales integer, in ip_driven_by varchar(40))
    sp_main: begin
	-- ensure new van doesn't already exist
    -- ensure that the delivery service exists
    -- ensure that a valid driver will control the van
    DECLARE service_id_count INT;
    DECLARE van_id_tag_count INT;
    DECLARE driver_username_count INT;
    DECLARE home_base_location varchar(40);

SELECT COUNT(*) INTO service_id_count
FROM delivery_services
WHERE id = ip_id;

IF service_id_count = 0 THEN
    leave sp_main;
END IF;

SELECT COUNT(*) INTO van_id_tag_count
FROM vans
WHERE id = ip_id AND tag = ip_tag;

IF van_id_tag_count > 0 THEN
    leave sp_main;
END IF;

SELECT COUNT(*) INTO driver_username_count
FROM employees
WHERE username = ip_driven_by;

IF driver_username_count = 0 THEN
    leave sp_main;
END IF;

SELECT home_base INTO home_base_location
FROM delivery_services
WHERE id = ip_id;

INSERT INTO vans (
    id,
    tag,
    fuel,
    capacity,
    sales,
    driven_by,
    located_at
)
VALUES (
           ip_id,
           ip_tag,
           ip_fuel,
           ip_capacity,
           ip_sales,
           ip_driven_by,
           home_base_location

       );
end //
delimiter ;

-- [7] add_business()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new business.  A new business must have a
unique (long) name and must exist at a valid location, and have a valid rating.
And a resturant is initially "independent" (i.e., no owner), but will be assigned
an owner later for funding purposes. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_business;
delimiter //
create procedure add_business (in ip_long_name varchar(40), in ip_rating integer,
                               in ip_spent integer, in ip_location varchar(40))
    sp_main: begin
	-- ensure new business doesn't already exist
    -- ensure that the location is valid
    -- ensure that the rating is valid (i.e., between 1 and 5 inclusively)
    IF ip_long_name NOT IN (Select long_name from businesses) and 
    ip_location IN (Select label from locations) and
    ip_rating BETWEEN 1 and 5 THEN
    INSERT INTO businesses VALUES(ip_long_name, ip_rating, ip_spent, ip_location);
END IF;
end //
delimiter ;

-- [8] add_service()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new delivery service.  A new service must have
a unique identifier, along with a valid home base and manager. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_service;
delimiter //
create procedure add_service (in ip_id varchar(40), in ip_long_name varchar(100),
                              in ip_home_base varchar(40), in ip_manager varchar(40))
    sp_main: begin
	-- ensure new delivery service doesn't already exist
    -- ensure that the home base location is valid
    -- ensure that the manager is valid
    IF NOT EXISTS(Select 1 from delivery_services where id=ip_id) and
    EXISTS(Select 1 from locations where label=ip_home_base) and
    NOT EXISTS(Select 1 from delivery_services where manager=ip_manager)
    THEN
    INSERT INTO delivery_services VALUES(ip_id, ip_long_name, ip_home_base, ip_manager);
END IF;
end //
delimiter ;

-- [9] add_location()
-- -----------------------------------------------------------------------------
/* This stored procedure creates a new location that becomes a new valid van
destination.  A new location must have a unique combination of coordinates. */
-- -----------------------------------------------------------------------------
drop procedure if exists add_location;
delimiter //
create procedure add_location (in ip_label varchar(40), in ip_x_coord integer,
                               in ip_y_coord integer, in ip_space integer)
    sp_main: begin
	-- ensure new location doesn't already exist
    -- ensure that the coordinate combination is distinct
    DECLARE label_count INT;
    DECLARE coord_count INT;
    
    
	-- Check if information already exists
SELECT COUNT(*) INTO label_count FROM locations WHERE label = ip_label;
SELECT COUNT(*) INTO coord_count FROM locations WHERE x_coord = ip_x_coord AND y_coord = ip_y_coord;

-- If the label doesn't exist and the coordinate combination is unique then add the new location
IF label_count = 0 AND coord_count = 0 THEN
        INSERT INTO locations (label, x_coord, y_coord, space)
        VALUES (ip_label, ip_x_coord, ip_y_coord, ip_space);
END IF;
end //
delimiter ;

-- [10] start_funding()
-- -----------------------------------------------------------------------------
/* This stored procedure opens a channel for a business owner to provide funds
to a business. The owner and business must be valid. */
-- -----------------------------------------------------------------------------
drop procedure if exists start_funding;
delimiter //
create procedure start_funding (in ip_owner varchar(40), in ip_amount integer, in ip_long_name varchar(40), in ip_fund_date date)
    sp_main: begin
	-- ensure the owner and business are valid
    IF EXISTS(Select 1 from businesses where long_name=ip_long_name) and 
    EXISTS(Select 1 from business_owners where ip_owner=username) THEN
    INSERT INTO fund VALUES(ip_owner, ip_amount, ip_fund_date, ip_long_name);
END IF;
end //
delimiter ;

-- [11] hire_employee()
-- -----------------------------------------------------------------------------
/* This stored procedure hires a worker to work for a delivery service.
If a worker is actively serving as manager for a different service, then they are
not eligible to be hired.  Otherwise, the hiring is permitted. */
-- -----------------------------------------------------------------------------
drop procedure if exists hire_employee;
delimiter //
create procedure hire_employee (in ip_username varchar(40), in ip_id varchar(40))
    sp_main: begin
	-- ensure that the employee hasn't already been hired by that service
	-- ensure that the employee and delivery service are valid
    -- ensure that the employee isn't a manager for another 
    IF NOT EXISTS(Select 1 from work_for where username=ip_username and id=ip_id) and
    ip_username IN (Select username from employees) and 
    ip_id IN (Select id from delivery_services) and
    NOT EXISTS(Select 1 from delivery_services where id<>ip_id and manager=ip_username)
    THEN
    INSERT INTO work_for VALUES(ip_username, ip_id);
END IF;
end //
delimiter ;

-- [12] fire_employee()
-- -----------------------------------------------------------------------------
/* This stored procedure fires a worker who is currently working for a delivery
service.  The only restriction is that the employee must not be serving as a manager 
for the service. Otherwise, the firing is permitted. */
-- -----------------------------------------------------------------------------
drop procedure if exists fire_employee;
delimiter //
create procedure fire_employee (in ip_username varchar(40), in ip_id varchar(40))
    sp_main: begin
	-- ensure that the employee is currently working for the service
    -- ensure that the employee isn't an active manager
    IF EXISTS(Select 1 from work_for where ip_username=username and ip_id=id) and
    NOT EXISTS(Select 1 from delivery_services where ip_username=manager) THEN
DELETE FROM work_for WHERE username=ip_username and id=ip_id;
END IF;
end //
delimiter ;

-- [13] manage_service()
-- -----------------------------------------------------------------------------
/* This stored procedure appoints a worker who is currently hired by a delivery
service as the new manager for that service.  The only restrictions is that
the worker must not be working for any other delivery service. Otherwise, the appointment 
to manager is permitted.  The current manager is simply replaced. */
-- -----------------------------------------------------------------------------
drop procedure if exists manage_service;
delimiter //
create procedure manage_service (in ip_username varchar(40), in ip_id varchar(40))
    sp_main: begin
	-- ensure that the employee is currently working for the service
    -- ensure that the employee isn't working for any other services
    
    DECLARE worker_count INT;
    DECLARE other_services_count INT;

    -- ensure that the employee is currently working for the service
SELECT COUNT(*) INTO worker_count
FROM work_for
WHERE username = ip_username AND id = ip_id;

-- ensure that the employee isn't working for any other services
SELECT COUNT(*) INTO other_services_count
FROM work_for
WHERE username = ip_username AND id != ip_id;

-- If conditions are met, appoint the worker as the new manager
IF worker_count > 0 AND other_services_count = 0 THEN
UPDATE delivery_services
SET manager = ip_username
WHERE id = ip_id;
END IF;
end //
delimiter ;

-- [14] takeover_van()
-- -----------------------------------------------------------------------------
/* This stored procedure allows a valid driver to take control of a van owned by 
the same delivery service. The current controller of the van is simply relieved 
of those duties. */
-- -----------------------------------------------------------------------------
drop procedure if exists takeover_van;
delimiter //
create procedure takeover_van (in ip_username varchar(40), in ip_id varchar(40),
                               in ip_tag integer)
    sp_main: begin
	-- ensure that the driver is not driving for another service
	-- ensure that the selected van is owned by the same service
    -- ensure that the employee is a valid driver
    if not exists (
        select 1
        from drivers d
        where d.username = ip_username
    ) then
        leave sp_main;
end if;

    if not exists (
        select 1
        from vans
        where id = ip_id and tag = ip_tag
    ) then
        leave sp_main;
end if;

    if exists (
        select 1
        from vans
        where driven_by = ip_username and id != ip_id
    ) then
        leave sp_main;
end if;

update vans
set driven_by = ip_username
where id = ip_id and tag = ip_tag;
end //
delimiter ;

-- [15] load_van()
-- -----------------------------------------------------------------------------
/* This stored procedure allows us to add some quantity of fixed-size packages of
a specific product to a van's payload so that we can sell them for some
specific price to other businesses.  The van can only be loaded if it's located
at its delivery service's home base, and the van must have enough capacity to
carry the increased number of items.

The change/delta quantity value must be positive, and must be added to the quantity
of the product already loaded onto the van as applicable.  And if the product
already exists on the van, then the existing price must not be changed. */
-- -----------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS load_van;
DELIMITER //
CREATE PROCEDURE load_van (
    IN van_id VARCHAR(40),
    IN van_tag INT,
    IN product_barcode VARCHAR(40),
    IN new_package_qty INT,
    IN package_price INT
)
    proc_block: BEGIN
    IF (van_id, van_tag) NOT IN (SELECT id, tag FROM vans) THEN LEAVE proc_block;
END IF;
    IF product_barcode NOT IN (SELECT barcode FROM products) THEN LEAVE proc_block;
END IF;
    IF (SELECT located_at FROM vans WHERE id = van_id AND tag = van_tag) 
       <> (SELECT home_base FROM delivery_services WHERE id = van_id) THEN LEAVE proc_block;
END IF;

    IF new_package_qty <= 0 THEN LEAVE proc_block;
END IF;

  
    IF (SELECT SUM(quantity)
        FROM contain WHERE id = van_id AND tag = van_tag) + new_package_qty 
       > (SELECT capacity FROM vans WHERE id = van_id AND tag = van_tag) THEn LEAVE proc_block;
END IF;
    IF product_barcode IN (SELECT barcode FROM contain where id = van_id AND tag = van_tag) THEN UPDATE contain SET quantity = quantity + new_package_qty
                                                                                                 WHERE id = van_id AND tag = van_tag AND barcode = product_barcode;
LEAVE proc_block;
END IF;

INSERT INTO contain (id, tag, barcode, quantity, price)
VALUES (van_id, van_tag, product_barcode, new_package_qty, package_price);
END //
DELIMITER ;


-- [16] refuel_van()
-- -----------------------------------------------------------------------------
/* This stored procedure allows us to add more fuel to a van. The van can only
be refueled if it's located at the delivery service's home base. */
-- -----------------------------------------------------------------------------
drop procedure if exists refuel_van;
delimiter //
create procedure refuel_van (in ip_id varchar(40), in ip_tag integer, in ip_more_fuel integer)
    sp_main: begin
	-- ensure that the van being switched is valid and owned by the service
    -- ensure that the van is located at the service home base
    IF EXISTS(Select 1 from vans where id=ip_id and tag=ip_tag) and
    EXISTS(Select 1 from vans v join delivery_services ds ON v.id=ds.id where ds.home_base=v.located_at)
    THEN
UPDATE vans SET fuel=fuel+ip_more_fuel WHERE id=ip_id and tag=ip_tag;
END IF;
end //
delimiter ;

-- [17] drive_van()
-- -----------------------------------------------------------------------------
/* This stored procedure allows us to move a single van to a new
location (i.e., destination). This will also update the respective driver's 
experience and van's fuel. The main constraints on the van(s) being able to 
move to a new  location are fuel and space.  A van can only move to a destination
if it has enough fuel to reach the destination and still move from the destination
back to home base.  And a van can only move to a destination if there's enough
space remaining at the destination. */
-- -----------------------------------------------------------------------------
drop function if exists fuel_required;
delimiter //
create function fuel_required (ip_departure varchar(40), ip_arrival varchar(40))
    returns integer reads sql data
begin
	if (ip_departure = ip_arrival) then return 0;
else return (select 1 + truncate(sqrt(power(arrival.x_coord - departure.x_coord, 2) + power(arrival.y_coord - departure.y_coord, 2)), 0) as fuel
		from (select x_coord, y_coord from locations where label = ip_departure) as departure,
        (select x_coord, y_coord from locations where label = ip_arrival) as arrival);
end if;
end //
delimiter ;

DROP PROCEDURE IF EXISTS drive_van;
DELIMITER //
CREATE PROCEDURE drive_van (
    IN van_id VARCHAR(40),
    IN van_tag INT,
    IN destination_label VARCHAR(40)
)
    proc_block: BEGIN

    DECLARE base_location VARCHAR(40);
    DECLARE current_position VARCHAR(40);
    DECLARE fuel_needed_to_dest INT;
    DECLARE fuel_needed_to_base INT;
    DECLARE fuel_available INT;
    DECLARE space_available INT;

SELECT home_base INTO base_location FROM delivery_services WHERE id = van_id;
SELECT located_at, fuel INTO current_position, fuel_available FROM vans WHERE id = van_id AND tag = van_tag;
-- Check of location is valid
IF destination_label NOT IN (SELECT label FROM locations) THEN LEAVE proc_block;
END IF;
    IF current_position = destination_label THEN LEAVE proc_block;
END IF;

    SET fuel_needed_to_dest = fuel_required(current_position, destination_label);
    SET fuel_needed_to_base = fuel_required(destination_label, base_location);
    
    -- Fuel check
    IF fuel_available < (fuel_needed_to_dest + fuel_needed_to_base) THEN LEAVE proc_block;
END IF;
SELECT space INTO space_available FROM locations WHERE label = destination_label;

IF space_available IS NOT NULL AND space_available < 1 THEN LEAVE proc_block;
END IF;

UPDATE vans SET located_at = destination_label, fuel = fuel - fuel_needed_to_dest
WHERE id = van_id AND tag = van_tag;

UPDATE drivers
SET successful_trips = successful_trips + 1
WHERE username = (SELECT driven_by FROM vans WHERE id = van_id AND tag = van_tag);

END //
DELIMITER ;


-- [18] purchase_product()
-- -----------------------------------------------------------------------------
/* This stored procedure allows a business to purchase products from a van
at its current location.  The van must have the desired quantity of the product
being purchased.  And the business must have enough money to purchase the
products.  If the transaction is otherwise valid, then the van and business
information must be changed appropriately.  Finally, we need to ensure that all
quantities in the payload table (post transaction) are greater than zero. */
-- -----------------------------------------------------------------------------
drop procedure if exists purchase_product;
delimiter //
create procedure purchase_product (in ip_long_name varchar(40), in ip_id varchar(40),
                                   in ip_tag integer, in ip_barcode varchar(40), in ip_quantity integer)
    sp_main: begin
	-- ensure that the business is valid
    -- ensure that the van is valid and exists at the business's location
	-- ensure that the van has enough of the requested product
	-- update the van's payload
    -- update the monies spent and gained for the van and business
    -- ensure all quantities in the contain table are greater than zero
    declare product_price integer;	
    declare total_cost integer;
    
    if ip_long_name is null or ip_id is null or ip_tag is null or 
       ip_barcode is null or ip_quantity is null or ip_quantity <= 0 then
	leave sp_main;
end if;
    
    if ip_long_name not in (select long_name from businesses) then
        leave sp_main;
end if;
    
    if (ip_id, ip_tag) not in (
        select v.id, v.tag 
        from vans v
        join businesses b on v.located_at = b.location
        where b.long_name = ip_long_name
    ) then
        leave sp_main;
end if;
    
    if (ip_id, ip_tag, ip_barcode) not in (
        select id, tag, barcode 
        from contain
    ) then
        leave sp_main;
end if;

    if ip_quantity > (
        select quantity 
        from contain 
        where id = ip_id and tag = ip_tag and barcode = ip_barcode
    ) then
        leave sp_main;
end if;

select price into product_price
from contain
where id = ip_id and tag = ip_tag and barcode = ip_barcode;
set total_cost = product_price * ip_quantity;

update contain
set quantity = quantity - ip_quantity
where id = ip_id and tag = ip_tag and barcode = ip_barcode;

update vans
set sales = sales + total_cost
where id = ip_id and tag = ip_tag;

update businesses
set spent = spent + total_cost
where long_name = ip_long_name;
delete from contain where quantity <= 0;
end //
delimiter ;

-- [19] remove_product()
-- -----------------------------------------------------------------------------
/* This stored procedure removes a product from the system.  The removal can
occur if, and only if, the product is not being carried by any vans. */
-- -----------------------------------------------------------------------------
drop procedure if exists remove_product;
delimiter //
create procedure remove_product (in ip_barcode varchar(40))
    sp_main: begin
	-- ensure that the product exists
    -- ensure that the product is not being carried by any vans
    IF ip_barcode IN (Select barcode from products) and
    ip_barcode NOT IN (Select barcode from contain)
    THEN
DELETE FROM products WHERE barcode=ip_barcode;
END IF;
end //
delimiter ;

-- [20] remove_van()
-- -----------------------------------------------------------------------------
/* This stored procedure removes a van from the system.  The removal can
occur if, and only if, the van is not carrying any products.*/
-- -----------------------------------------------------------------------------
drop procedure if exists remove_van;
delimiter //
create procedure remove_van (in ip_id varchar(40), in ip_tag integer)
    sp_main: begin
	-- ensure that the van exists
    -- ensure that the van is not carrying any products
    IF EXISTS(Select 1 from vans where id=ip_id and tag=ip_tag) and
    NOT EXISTS(Select 1 from contain where id=ip_id and tag=ip_tag and quantity>0)
    THEN
    #DELETE FROM contain WHERE tag=ip_tag and id=ip_id;
DELETE FROM vans WHERE tag=ip_tag and id=ip_id;
END IF;
end //
delimiter ;

-- [21] remove_driver_role()
-- -----------------------------------------------------------------------------
/* This stored procedure removes a driver from the system.  The removal can
occur if, and only if, the driver is not controlling any vans.  
The driver's information must be completely removed from the system. */
-- -----------------------------------------------------------------------------
drop procedure if exists remove_driver_role;
delimiter //
create procedure remove_driver_role (in ip_username varchar(40))
    sp_main: begin
	-- ensure that the driver exists
    -- ensure that the driver is not controlling any vans
    -- remove all remaining information
    
    DECLARE driver_count INT;
    DECLARE van_count INT;
    
    -- select the information
SELECT COUNT(*) INTO driver_count FROM drivers WHERE username = ip_username;
SELECT COUNT(*) INTO van_count FROM vans WHERE driven_by = ip_username;

IF driver_count > 0 AND van_count = 0 THEN

DELETE FROM drivers WHERE username = ip_username;
DELETE FROM work_for WHERE username = ip_username;

-- Check if the employee is also a worker
IF NOT EXISTS (SELECT 1 FROM workers WHERE username = ip_username) THEN

DELETE FROM employees WHERE username = ip_username;
DELETE FROM users WHERE username = ip_username;
END IF;
END IF;
end //
delimiter ;

-- [22] display_owner_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of an owner.
For each owner, it includes the owner's information, along with the number of
businesses for which they provide funds and the number of different places where
those businesses are located.  It also includes the highest and lowest ratings
for each of those businesses, as well as the total amount of debt based on the
monies spent purchasing products by all of those businesses. And if an owner
doesn't fund any businesses then display zeros for the highs, lows and debt. */
-- -----------------------------------------------------------------------------
create or replace view display_owner_view as
SELECT
    bo.username AS username,
    u.first_name,
    u.last_name,
    u.address,
    COUNT(DISTINCT f.business) AS num_businesses,
    COUNT(DISTINCT b.location) AS diff_places,
    COALESCE(MAX(b.rating), 0) AS high_ratings,
    COALESCE(MIN(b.rating), 0) AS low_ratings,
    COALESCE(SUM(b.spent), 0) AS total_debt
FROM business_owners bo LEFT JOIN
     fund f ON bo.username = f.username
                        LEFT JOIN
     users u ON bo.username = u.username
                        LEFT JOIN
     businesses b ON f.business = b.long_name
GROUP BY
    bo.username, u.first_name, u.last_name, u.address;

-- [23] display_employee_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of an employee.
For each employee, it includes the username, tax identifier, hiring date and
experience level, along with the license identifer and drivering experience (if
applicable), and a 'yes' or 'no' depending on the manager status of the employee. */
-- -----------------------------------------------------------------------------
create or replace view display_employee_view as
Select e.username, e.taxID, e.salary, e.hired, e.experience, COALESCE(licenseID, 'n/a'), COALESCE(d.successful_trips, 'n/a'),
       CASE WHEN ds.manager IS NULL THEN 'no' ELSE 'yes' END AS "manager"
from employees e
         left join drivers d ON d.username=e.username
         left join delivery_services ds ON ds.manager=e.username;

-- [24] display_driver_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a driver.
For each driver, it includes the username, licenseID and drivering experience, along
with the number of vans that they are controlling. */
-- -----------------------------------------------------------------------------
create or replace view display_driver_view as
select username, licenseID, successful_trips, count(driven_by)
from drivers d
         left join vans v ON v.driven_by=d.username
group by 1,2,3;


-- [25] display_location_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a location.
For each location, it includes the label, x- and y- coordinates, along with the
name of the business or service at that location, the number of vans as well as 
the identifiers of the vans at the location (sorted by the tag), and both the 
total and remaining capacity at the location. */
-- -----------------------------------------------------------------------------
create or replace view display_location_view as
SELECT
    l.label AS location_label,
    b.business_name,
    l.x_coord AS x_coordinate,
    l.y_coord AS y_coordinate,
    l.space AS total_space,
    v.van_count AS total_vans,
    v.van_list AS van_identifiers,
    l.space - v.van_count AS available_capacity
FROM
    locations l
        INNER JOIN (
        SELECT
            business.long_name AS business_name,
            business.location_name
        FROM (
                 SELECT long_name, location AS location_name
                 FROM businesses
                 UNION
                 SELECT ds.long_name, ds.home_base AS location_name
                 FROM delivery_services ds
             ) business
    ) b
                   ON l.label = b.location_name
        INNER JOIN (
        SELECT
            vns.located_at AS location_label,
            COUNT(*) AS van_count,
            GROUP_CONCAT(CONCAT(vns.id, vns.tag) ORDER BY vns.tag ASC) AS van_list
        FROM vans vns
        GROUP BY vns.located_at
    ) v
                   ON l.label = v.location_label;

-- [26] display_product_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of the products.
For each product that is being carried by at least one van, it includes a list of
the various locations where it can be purchased, along with the total number of packages
that can be purchased and the lowest and highest prices at which the product is being
sold at that location. */
-- -----------------------------------------------------------------------------
create or replace view display_product_view as
select
    p.iname as product_name,
    l.label as location,
    sum(c.quantity) as total_packages,
    min(c.price) as min_price,
    max(c.price) as max_price
from
    products p
        join
    contain c on p.barcode = c.barcode
        join
    vans v on c.id = v.id and c.tag = v.tag
        join
    locations l on v.located_at = l.label
group by
    p.iname, l.label;

-- [27] display_service_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a delivery
service.  It includes the identifier, name, home base location and manager for the
service, along with the total sales from the vans.  It must also include the number
of unique products along with the total cost and weight of those products being
carried by the vans. */
-- -----------------------------------------------------------------------------
create or replace view display_service_view as
select ds.id, ds.long_name, ds.home_base, ds.manager,
       v.total_sales AS "total_sales",
       pc.unique_products AS "unique_products",
       pc.total_cost AS "total_cost",
       pc.total_weight AS "total_weight"
from delivery_services ds
         left join (Select id, SUM(sales) as total_sales
                    from vans
                    group by 1) v ON v.id=ds.id
         left join (Select id, COUNT(DISTINCT c.barcode) as unique_products,
                           SUM(price*quantity) as total_cost,
                           SUM(weight*quantity) as total_weight
                    from contain c
                             left join products p ON p.barcode=c.barcode
                    group by 1) pc ON pc.id=ds.id
-- select ds.id, ds.long_name, ds.home_base, ds.manager, 
-- SUM(v.sales) AS "total_sales", 
-- COUNT(DISTINCT c.barcode) AS "unique_products",
-- SUM(c.quantity*c.price) AS "total_cost",
-- SUM(p.weight*c.quantity) AS "total_weight"
-- from delivery_services ds
-- left join vans v ON v.id=ds.id
-- left join contain c ON c.id=v.id and c.tag=v.tag
-- left join products p ON p.barcode=c.barcode
-- group by 1,2,3,4;
