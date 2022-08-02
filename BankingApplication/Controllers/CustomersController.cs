using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;

namespace BankingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BankingContext _context;   //Creating object of the context class

        public CustomersController(BankingContext context)  //Initialisation of the object using parameterised constructor
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]           //To fetch the data in the Customers table
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]   //To fetch the data from the customers table based on the id number(basically fetching the details of a customer)
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);  //The variable customer stores a customer object

            if (customer == null)           //Will check that the id must be present
            {
                return NotFound();
            }

            return customer;                //Returns the customer with the required id
        }

        // PUT: api/Customers/5
        
        [HttpPut("{id}")]                   //Updating a record, basically editing a record
        public async Task<IActionResult> PutCustomer(int id,Customer customer) 
                                            //Editing a record of a customer with the given id
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            
            Customer cust = _context.Customers.Find(id);
            cust.FirstName = customer.FirstName;
            cust.MiddleName = customer.MiddleName;
            cust.LastName = customer.LastName;
            cust.FatherName = customer.FatherName;
            cust.AadharNumber = customer.AadharNumber;
            cust.MobileNumber = customer.MobileNumber;
            cust.OccupationType = customer.OccupationType;
            cust.GrossAnnualIncome = customer.GrossAnnualIncome;
            cust.AddressLine1 = customer.AddressLine1;
            cust.AddressLine2 = customer.AddressLine2;
            cust.City = customer.City;
            cust.State = customer.State;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);  
                        //Creates an object that produces 201 status/Created successfully
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();     //Status code 204-the deletion successfully done
        }

        private bool CustomerExists(int id)         //To check presence of the record of a particular customer
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
