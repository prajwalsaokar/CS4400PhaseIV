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
    -- ensure parameters not null
    if ip_username is null or ip_first_name is null or ip_last_name is null or ip_address is null or
       ip_birthdate is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure new owner has a unique username
    if ip_username in (select username from users) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    if ip_username in (select username from business_owners) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if; -- in theory, not needed but it's an extra check

    insert into users values (ip_username, ip_first_name, ip_last_name, ip_address, ip_birthdate);
    insert into business_owners values (ip_username);
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
    -- ensure parameters not null
    if ip_username is null or ip_first_name is null or ip_last_name is null or ip_address is null
        or ip_birthdate is null or ip_taxID is null or ip_hired is null or ip_employee_experience is null
        or ip_salary is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure new employee has a unique username
    -- ensure new employee has a unique tax identifier
    if ip_username in (select username from users) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    if ip_username in (select username from employees) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if; -- in theory, not needed but it's an extra check
    if ip_taxID in (select taxID from employees) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- add employee info
    insert into users values (ip_username, ip_first_name, ip_last_name, ip_address, ip_birthdate);
    insert into employees values (ip_username, ip_taxID, ip_hired, ip_employee_experience, ip_salary);
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
    -- ensure parameters not null
    if ip_username is null or ip_licenseID is null or ip_license_type is null or ip_driver_experience is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure that the user exists
    if ip_username not in (select username from users) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
        -- Ensure that the user is an employee
    elseif ip_username not in (select username from employees) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
        -- Ensure that the licenseID is unique among other drivers
    elseif ip_licenseID in (select licenseID from drivers where username != ip_username) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Check if the employee is already a driver
        if ip_username in (select username from drivers) then
            -- Update driver information
            update drivers
            set licenseID = ip_licenseID,
                license_type = ip_license_type,
                successful_trips = ip_driver_experience
            where username = ip_username;
        else
            -- Insert into drivers table
            insert into drivers (username, licenseID, license_type, successful_trips)
            values (ip_username, ip_licenseID, ip_license_type, ip_driver_experience);
        end if;
    end if;
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
    -- ensure parameters not null
    if ip_username is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure the user exists
    if ip_username not in (select username from users) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure the employee exists
    if ip_username not in (select username from employees) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure the employee is not already a worker
    if ip_username in (select username from workers) then
        SIGNAL SQLSTATE '45000'SET MESSAGE_TEXT = 'Invalid input.';;
    end if;

    -- Insert into workers table
    insert into workers values (ip_username);
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
    -- ensure parameters not null
    if ip_barcode is null or ip_name is null then
        SIGNAL SQLSTATE '45000'SET MESSAGE_TEXT = 'Invalid input.';;
    end if;
    -- Check if the product already exists
    if ip_barcode in (select barcode from products) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Insert the product
        insert into products (barcode, iname, weight)
        values (ip_barcode, ip_name, ip_weight);
    end if;
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
    declare home_base_van varchar(40);
    -- ensure parameters not null
    if ip_id is null or ip_tag is null or ip_fuel is null or ip_capacity is null or ip_sales is null
        or ip_driven_by is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Check if the van tag is unique
    if ip_id not in (select id from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    elseif ip_tag in (select tag from vans) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
        -- Check if the driver exists
    elseif ip_driven_by not in (select username from drivers) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Insert the van
        select home_base into home_base_van from delivery_services where id = ip_id limit 1;
        insert into vans (id, tag, fuel, capacity, sales, driven_by, located_at)
        values (ip_id, ip_tag, ip_fuel, ip_capacity, ip_sales, ip_driven_by, home_base_van);
    END IF;
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
    -- ensure parameters not null
    if ip_long_name is null or ip_rating is null or ip_spent is null
        or ip_location then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure business name is unique
    if ip_long_name in (select long_name from businesses) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure location is valid
    if ip_location not in (select label from locations) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Ensure rating is between 1 and 5
    if ip_rating < 1 or ip_rating > 5 then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- Insert new business
    insert into businesses (long_name, rating, spent, location)
    values (ip_long_name, ip_rating, ip_spent, ip_location);
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
    -- ensure parameters not null
    if ip_id is null or ip_long_name is null or ip_home_base is null or ip_manager is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Check if the service ID already exists
    if ip_id in (select id from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
        -- Check if the service name already exists
    elseif ip_long_name in (select long_name from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Insert the new service
        INSERT INTO delivery_services (id, long_name, home_base, manager)
        VALUES (ip_id, ip_long_name, ip_home_base, ip_manager);
    end if;
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
    -- ensure parameters not null
    if ip_label is null or ip_x_coord is null or ip_y_coord is null or ip_space is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Check if the location ID already exists
    if ip_label in (select label from locations) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    elseif (ip_x_coord, ip_y_coord) in (select x_coord, y_coord from locations) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Insert the new location
        insert into locations (label, x_coord, y_coord, space)
        values (ip_label, ip_x_coord, ip_y_coord, ip_space);
    end if;
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
    -- ensure parameters not null
    if ip_owner is null or ip_amount is null or ip_long_name is null or ip_fund_date is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- Check if the company exists
    if ip_long_name not in (select long_name from businesses) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
        -- Check if the investor exists
    elseif ip_owner not in (select username from business_owners) THEN
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    else
        -- Insert the funding record
        insert into fund (username, invested, invested_date, business)
        values (ip_owner, ip_amount, ip_fund_date, ip_long_name);
    end if;
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
    -- ensure parameters are not null
    if ip_username is null or ip_id is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee hasn't already been hired by that service
    if ip_username in (select username from work_for where id=ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee and delivery service are valid
    if ip_username not in (select username from employees) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    if ip_id not in (select id from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee isn't a manager for another service
    if ip_username in (select manager from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- otherwise, hiring is permitted
    insert into work_for values(ip_username, ip_id);
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
    -- ensure parameters are not null
    if ip_username is null or ip_id is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee is currently working for the service
    if ip_username not in (select username from work_for) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee isn't an active manager
    if ip_username in (select manager from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- otherwise, firing is permitted
    delete from work_for where username = ip_username and id = ip_id;
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
    -- ensure parameters are not null
    if ip_username is null or ip_id is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee isn't working for any other services
    if ip_username in (select username from work_for where id != ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee is currently working for the service
    if ip_username not in (select username from work_for where id = ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- otherwise, the appointment to manager is permitted
    -- since the current manager is replaced, we can just use update
    update delivery_services set manager = ip_username where id = ip_id;
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
    -- ensure parameters are not null
    if ip_username is null or ip_id is null or ip_tag is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the driver is not driving for another service
    if ip_username in (select username from work_for where id != ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the selected van is owned by the same service
    if ip_tag not in (select tag from vans where id = ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the employee is a valid driver
    if ip_username not in (select username from drivers where licenseID is not null or license_type is not null) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- driver is allowed to take control
    -- since the current controller of the van is simply relieved of those duties, we can use update
    update vans set driven_by = ip_username where tag = ip_tag and id = ip_id;
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
drop procedure if exists load_van;
delimiter //
create procedure load_van (in ip_id varchar(40), in ip_tag integer, in ip_barcode varchar(40),
                           in ip_more_packages integer, in ip_price integer)
sp_main: begin
    -- declare variables
    declare vanLocatedAt varchar(40);
    declare vanCapacity integer;

    -- ensure parameters are not null
    if ip_id is null or ip_tag is null or ip_barcode is null or ip_more_packages is null or ip_price is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van being loaded is owned by the service
    if ip_tag not in (select tag from vans where id = ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the product is valid
    if ip_barcode not in (select barcode from products) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van is located at the service home base
    set vanLocatedAt = (select located_at from vans where id = ip_id and tag = ip_tag);
    if vanLocatedAt not in (select home_base from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the quantity of new packages is greater than zero
    if ip_more_packages = 0 then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van has sufficient capacity to carry the new packages
    set vanCapacity = (select capacity from vans where id = ip_id and tag = ip_tag);
    if vanCapacity < ip_more_packages then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- add more of the product to the van
    -- if the product is already loaded
    if ip_barcode in (select barcode from contain where ip_id = id and ip_tag = tag) then
        -- new packages must be added to the quantity of the product already loaded onto the van so update
        update contain set quantity = ip_more_packages + quantity
        where ip_barcode = barcode and ip_id = id and ip_tag = tag;
        -- otherwise, since the product is not already loaded, insert into contain table
    else
        insert into contain value(ip_id, ip_tag, ip_barcode, ip_more_packages, ip_price);
    end if;
end //
delimiter ;

-- [16] refuel_van()
-- -----------------------------------------------------------------------------
/* This stored procedure allows us to add more fuel to a van. The van can only
be refueled if it's located at the delivery service's home base. */
-- -----------------------------------------------------------------------------
drop procedure if exists refuel_van;
delimiter //
create procedure refuel_van (in ip_id varchar(40), in ip_tag integer, in ip_more_fuel integer)
sp_main: begin
    -- declare variables
    declare vanLocatedAt varchar(40);

    -- ensure parameters are not null
    if ip_id is null or ip_tag is null or ip_more_fuel is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van being switched is valid and owned by the service
    if ip_tag not in (select tag from vans where id = ip_id) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van is located at the service home base
    set vanLocatedAt = (select located_at from vans where id = ip_id and tag = ip_tag);
    if vanLocatedAt not in (select home_base from delivery_services) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- van can be refueled
    update vans set fuel = ip_more_fuel + fuel
    where id = ip_id and tag = ip_tag;
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

drop procedure if exists drive_van;
delimiter //
create procedure drive_van (in ip_id varchar(40), in ip_tag integer, in ip_destination varchar(40))
sp_main: begin
    -- declare variables
    declare vanLocatedAt varchar(40);
    declare vanFuel integer;
    declare homeBase varchar(40);

    -- ensure parameters are not null
    if ip_id is null or ip_tag is null or ip_destination is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the destination is a valid location
    if ip_destination not in (select label from locations) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van isn't already at the location
    set vanLocatedAt = (select located_at from vans where id = ip_id and tag = ip_tag);
    if ip_destination = vanLocatedAt then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van has enough fuel to reach the destination and (then) home base
    set vanFuel = (select fuel from vans where id = ip_id and tag = ip_tag);
    set homeBase = (select home_base from delivery_services where id = ip_id);
    if vanFuel < (fuel_required(vanLocatedAt, ip_destination) + fuel_required(ip_destination, homeBase)) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van has enough space at the destination for the trip
    if (select space from locations where label = ip_destination) = 0 then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- update driver's experience (successful trips)
    update drivers set successful_trips = successful_trips + 1
    where username in (select driven_by from vans where id = ip_id and tag = ip_tag);

    -- also, update van's fuel and destination
    update vans set fuel = fuel - fuel_required(vanLocatedAt, ip_destination), located_at = ip_destination
    where id = ip_id and tag = ip_tag;
end //
delimiter ;

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
    -- declare variables
    declare vanLocatedAt varchar(40);
    declare businessLocation varchar(40);
    declare vanQuantity integer;
    declare productCost integer;
    declare totalCost integer;
    declare businessFunds integer;
    declare busFunds int;
    declare totalSpent int;

    -- ensure parameters not null
    if ip_long_name is null or ip_id is null or ip_tag is null or ip_barcode is null or ip_quantity is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the business is valid
    if ip_long_name not in (select long_name from businesses) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure business has enough money to purchase products
    set productCost = (select price from contain where id = ip_id and tag = ip_tag and barcode = ip_barcode);
    IF productCost is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end IF;
    set totalCost = productCost * ip_quantity;
    set busFunds = (select sum(invested) from fund where business = ip_long_name);
    set totalSpent = (select spent from businesses where long_name = ip_long_name);
    if busFunds - totalSpent < totalCost then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van is valid and exists at the business's location
    set vanLocatedAt = (select located_at from vans where id = ip_id and tag = ip_tag);
    set businessLocation = (select location from businesses where long_name = ip_long_name);
    if vanLocatedAt != businessLocation then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van has enough of the requested product
    set vanQuantity = (select quantity from contain where id = ip_id and tag = ip_tag and barcode = ip_barcode);
    if vanQuantity < ip_quantity then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- update the van's payload
    update contain set quantity = quantity - ip_quantity
    where barcode = ip_barcode and id = ip_id and tag = ip_tag;
    -- update the monies spent and gained for the van and business
    update businesses set spent = spent + totalCost where long_name = ip_long_name;
    update vans set sales = sales + totalCost where id = ip_id and tag = ip_tag;

    -- ensure all quantities in the contain table are greater than zero
    -- to do this, we can simply just delete the entries with quantities of zero
    delete from contain where quantity = 0;
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
    -- ensure parameters not null
    if ip_barcode is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the product exists
    if ip_barcode not in (select barcode from products) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the product is not being carried by any vans
    if ip_barcode in (select barcode from contain) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- remove product from system
    delete from products where barcode = ip_barcode;
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
    -- ensure parameters not null
    if ip_id is null or ip_tag is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van exists
    if (ip_id, ip_tag) not in (select id, tag from vans) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the van is not carrying any products
    if (ip_id, ip_tag) in (select id, tag from contain) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;

    -- remove van from system
    delete from vans where id = ip_id and tag = ip_tag;
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
    -- ensure parameter not null
    if ip_username is null then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the driver exists
    if ip_username not in (select username from drivers) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- ensure that the driver is not controlling any vans
    if ip_username in (select driven_by from vans) then
        SIGNAL SQLSTATE '45000'
SET MESSAGE_TEXT = 'Invalid input or operation.';
    end if;
    -- remove all remaining information
    -- instead of removing driver info from every table that the driver appears in,
    -- if we simply remove from the root users table, then all the information will be deleted itself
    -- through cascading
    delete from users where username = ip_username;
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
select buss.username, us.first_name, us.last_name, us.address, count(fd.username) as numOfBussinesses,
       count(b.location) as numOfPlaces, coalesce(max(b.rating), 0) as highs, coalesce(min(b.rating), 0) as lows, coalesce(sum(b.spent), 0) as debt
from business_owners buss left join users us on buss.username = us.username
                          left join fund fd on fd.username = buss.username
                          left join businesses b on b.long_name = fd.business
group by buss.username, us.last_name, us.first_name, us.address;

-- [23] display_employee_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of an employee.
For each employee, it includes the username, tax identifier, salary, hiring date and
experience level, along with license identifer and driving experience (if applicable,
'n/a' if not), and a 'yes' or 'no' depending on the manager status of the employee. */
-- -----------------------------------------------------------------------------
create or replace view display_employee_view as
select emp.username, emp.taxID, emp.salary, emp.hired, emp.experience,
       case when licenseID is NULL then 'n/a' else licenseID end as licenseIdentifier,
       case when successful_trips is NULL then 'n/a' else successful_trips end as drivingExperience,
       case when ds.manager = emp.username then 'yes' else 'no' end as manager_status
from employees emp left join drivers dr on emp.username = dr.username
                   left join delivery_services ds on emp.username = ds.manager;

-- [24] display_driver_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a driver.
For each driver, it includes the username, licenseID and drivering experience, along
with the number of vans that they are controlling. */
-- -----------------------------------------------------------------------------
create or replace view display_driver_view as
select dr.username, dr.licenseID, dr.successful_trips, count(v.driven_by) as numOfVans
from drivers dr left join vans v on dr.username = v.driven_by
group by dr.username, dr.licenseID, dr.successful_trips;

-- [25] display_location_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a location.
For each location, it includes the label, x- and y- coordinates, along with the
name of the business or service at that location, the number of vans as well as 
the identifiers of the vans at the location (sorted by the tag), and both the 
total and remaining capacity at the location. */
-- -----------------------------------------------------------------------------
create or replace view display_location_view as
select loc.label as label, COALESCE(businesses.long_name, delivery_services.long_name) AS long_name,
       loc.x_coord as x_coord, loc.y_coord as y_coord,
       loc.space AS space,
       COUNT(DISTINCT CONCAT(vans.id, vans.tag)) AS num_vans,
       GROUP_CONCAT(DISTINCT CONCAT(vans.id, vans.tag) ORDER BY vans.tag SEPARATOR ',') AS van_ids,
       (loc.space - COUNT(DISTINCT CONCAT(vans.id, vans.tag))) AS remaining_capacity
from locations loc
         left join businesses on loc.label = businesses.location
         left join delivery_services on loc.label = delivery_services.home_base
         join vans on loc.label = vans.located_at
group by loc.label, long_name, loc.x_coord, loc.y_coord, loc.space;

-- [26] display_product_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of the products.
For each product that is being carried by at least one van, it includes a list of
the various locations where it can be purchased, along with the total number of packages
that can be purchased and the lowest and highest prices at which the product is being
sold at that location. */
-- -----------------------------------------------------------------------------
create or replace view display_product_view as
SELECT products.iname as product_name, loc.label as location, SUM(contain.quantity) as amount_available,
       MIN(contain.price) as low_price, MAX(contain.price) as high_price
FROM products JOIN contain ON contain.barcode = products.barcode
              JOIN vans ON vans.id = contain.id AND vans.tag = contain.tag
              JOIN locations loc ON vans.located_at = loc.label
GROUP BY products.iname, loc.label;

-- [27] display_service_view()
-- -----------------------------------------------------------------------------
/* This view displays information in the system from the perspective of a delivery
service.  It includes the identifier, name, home base location and manager for the
service, along with the total sales from the vans.  It must also include the number
of unique products along with the total cost and weight of those products being
carried by the vans. */
-- -----------------------------------------------------------------------------
create or replace view display_service_view as
select s.id, s.long_name, s.home_base, s.manager, (SELECT SUM(sales) FROM vans WHERE id = s.id) as revenue, COUNT(DISTINCT contain.barcode) as products_carried, SUM(contain.price * contain.quantity) as cost_carried, SUM(products.weight * contain.quantity) as weight_carried
FROM delivery_services s LEFT JOIN vans on s.id = vans.id LEFT JOIN contain on vans.id = contain.id and vans.tag = contain.tag
                         LEFT JOIN products on contain.barcode = products.barcode
GROUP BY s.id, s.long_name, s.home_base, s.manager;
