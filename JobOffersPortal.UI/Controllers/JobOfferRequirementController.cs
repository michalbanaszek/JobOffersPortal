using JobOffersPortal.UI.Interfaces;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.DeleteJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.UpdateJobOfferRequirementMvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.UI.Controllers
{
    public class JobOfferRequirementController : WebControllerBase
    {
        private readonly IJobOfferMvcService _jobOfferService;
        private readonly IJobOfferRequirementMvcService _jobOfferRequirementService;

        public JobOfferRequirementController(IJobOfferRequirementMvcService jobOfferPropositionService, IJobOfferMvcService jobOfferService)
        {
            _jobOfferRequirementService = jobOfferPropositionService;
            _jobOfferService = jobOfferService;
        }

        // GET: JobOfferRequirement/CreateShowDetails/5
        public async Task<IActionResult> CreateShowDetails(string id)
        {
            var response = await _jobOfferService.GetByIdAsync(id);

            var requirementMapped = Mapper.Map<List<JobOfferJobOfferRequirementMvcDto>>(response.Requirements);

            var viewModel = new CreateDetailsJobOfferRequirementMvcViewModel() { JobOfferId = id, Requirements = requirementMapped };

            return View(viewModel);
        }

        // POST: JobOfferRequirement/CreateShowDetails/5
        [HttpPost]
        public async Task<IActionResult> CreateShowDetails(CreateDetailsJobOfferRequirementMvcViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _jobOfferRequirementService.AddAsync(viewModel.JobOfferId, viewModel.Content);
                ModelState.Clear();
            }

            var responseFromQuery = await _jobOfferService.GetByIdAsync(viewModel.JobOfferId);

            var requirementMapped = Mapper.Map<List<JobOfferJobOfferRequirementMvcDto>>(responseFromQuery.Requirements);

            return View(new CreateDetailsJobOfferRequirementMvcViewModel() { JobOfferId = viewModel.JobOfferId, Requirements = requirementMapped });
        }

        // GET: JobOfferRequirement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _jobOfferRequirementService.GetByIdAsync(id);

            var viewModel = Mapper.Map<UpdateJobOfferRequirementMvcViewModel>(response);

            return View(viewModel);
        }

        // POST: JobOfferRequirement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content")] UpdateJobOfferRequirementMvcViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _jobOfferRequirementService.UpdateAsync(viewModel.Id, viewModel.Content);

            return RedirectToAction("Index", "JobOffer");
        }

        // GET: JobOfferRequirement/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var responseFromQuery = await _jobOfferRequirementService.GetByIdAsync(id);

            var viewModel = Mapper.Map<JobOfferRequirementDeleteMvcViewModel>(responseFromQuery);

            return View(viewModel);
        }

        // POST: JobOfferRequirement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _jobOfferRequirementService.DeleteAsync(id);

            return RedirectToAction("Index", "JobOffer");
        }
    }
}