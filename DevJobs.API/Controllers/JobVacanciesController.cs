﻿using DevJobs.API.Entities;
using DevJobs.API.Persistence;

namespace DevJobs.API.Controllers;

using DevJobs.API.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class JobVacanciesController : ControllerBase
{
    private readonly DevJobsContext _context;

    public JobVacanciesController(DevJobsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var jobVacancies = _context.JobVacancies;

        return Ok(jobVacancies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var jobVacancy = _context.JobVacancies.SingleOrDefault(jv => jv.Id == id);

        if (jobVacancy == null) return NotFound();

        return Ok(jobVacancy);
    }

    [HttpPost]
    public IActionResult Post(AddJobVacancyInputModel model)
    {
        if (ModelState.IsValid)
        {
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange);

            _context.JobVacancies.Add(jobVacancy);

            return CreatedAtAction("GetById", new { id = jobVacancy.Id }, jobVacancy);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UpdateJobVacancyInputModel model)
    {
        if (ModelState.IsValid)
        {
            var jobVacancy = _context.JobVacancies.SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null) return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            return NoContent();
        }

        return BadRequest();
    }
}