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
    public class BeneficiariesController : ControllerBase
    {
        private readonly BankingContext _context;

        public BeneficiariesController(BankingContext context)
        {
            _context = context;
        }

        // GET: api/Beneficiaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> GetBeneficiaries()
        {
            return await _context.Beneficiaries.ToListAsync();
        }

        // GET: api/Beneficiaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beneficiary>> GetBeneficiary(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);

            if (beneficiary == null)
            {
                return NotFound();
            }

            return beneficiary;
        }

        // PUT: api/Beneficiaries/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeneficiary(int id, Beneficiary beneficiary)
        {
            if (id != beneficiary.AccountNumber)
            {
                return BadRequest();
            }

            Beneficiary benef = _context.Beneficiaries.Find(id);
            benef.BeneficiaryAccountNo = beneficiary.BeneficiaryAccountNo;
            benef.BeneficiaryName = beneficiary.BeneficiaryName;
            benef.NickName = beneficiary.NickName;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryExists(id))
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

        // POST: api/Beneficiaries
        
        [HttpPost]
        public async Task<ActionResult<Beneficiary>> PostBeneficiary(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BeneficiaryExists(beneficiary.AccountNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBeneficiary", new { id = beneficiary.AccountNumber }, beneficiary);
        }

        // DELETE: api/Beneficiaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeneficiaryExists(int id)
        {
            return _context.Beneficiaries.Any(e => e.AccountNumber == id);
        }
    }
}
