using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.DeleteJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.UpdateJobOfferMvc;

namespace WebApp.Controllers
{
    public class JobOfferController : WebControllerBase
    {
        private readonly string _companyId = "0f2de8b6-9160-4843-b36e-90372a3f8179";
        private readonly IJobOfferMvcService _jobOfferService;

        public JobOfferController(IJobOfferMvcService jobOfferService)
        {
            _jobOfferService = jobOfferService;        
        }

        // GET: JobOffer
        public async Task<ActionResult<PaginatedMvcList<JobOfferMvcViewModel>>> Index()
        {
             var viewModel = await _jobOfferService.GetAllByCompany(_companyId);          

            return View(viewModel);
        }

        // GET: JobOffer/Details/5
        public async Task<ActionResult<JobOfferMvcViewModel>> Details(string id)
        {
            var viewModel = await _jobOfferService.GetByIdAsync(id);
            
            return View(viewModel);
        }

        // GET: JobOffer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobOffer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobOfferMvcViewModel createJobOfferMvcViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createJobOfferMvcViewModel);
            }

            try
            {
                createJobOfferMvcViewModel.CompanyId = _companyId;
                var responseFromApi = await _jobOfferService.AddAsync(createJobOfferMvcViewModel);

                if (!responseFromApi.Success)
                {
                    foreach (var error in responseFromApi.Errors)
                    {
                        ModelState.AddModelError("ErrorFromApi", error);
                    }
                }
             
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: JobOffer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var response = await _jobOfferService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<UpdateJobOfferMvcViewModel>(response);

            return View(viewModel);
        }

        // POST: JobOffer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateJobOfferMvcViewModel updateJobOfferMvcViewModel)
        {
            updateJobOfferMvcViewModel.CompanyId = _companyId;
            var responseFromApi = await _jobOfferService.UpdateAsync(updateJobOfferMvcViewModel.Id, updateJobOfferMvcViewModel);

            if (!responseFromApi.Success)
            {
                return View(updateJobOfferMvcViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: JobOffer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var response = await _jobOfferService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<JobOfferDeleteMvcViewModel>(response);

            return View(viewModel);
        }

        // POST: JobOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _jobOfferService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

