using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.DeleteJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.UpdateJobOfferPropositionMvc;

namespace WebApp.Controllers
{
    public class JobOfferPropositionController : WebControllerBase
    {
        private readonly IJobOfferMvcService _jobOfferService;
        private readonly IJobOfferPropositionMvcService _jobOfferPropositionService;

        public JobOfferPropositionController(IJobOfferPropositionMvcService jobOfferPropositionService, IJobOfferMvcService jobOfferService)
        {
            _jobOfferPropositionService = jobOfferPropositionService;
            _jobOfferService = jobOfferService;
        }

        // GET: JobOfferProposition/CreateShowDetails/5
        public async Task<IActionResult> CreateShowDetails(string id)
        {
            var response = await _jobOfferService.GetByIdAsync(id);

            var propositionMapped = Mapper.Map<List<JobOfferJobOfferPropositionMvcDto>>(response.Propositions);

            var viewModel = new CreateDetailsJobOfferPropositionMvcViewModel() { JobOfferId = id, Propositions = propositionMapped };

            return View(viewModel);
        }

        // POST: JobOfferProposition/CreateShowDetails/5
        [HttpPost]
        public async Task<IActionResult> CreateShowDetails(CreateDetailsJobOfferPropositionMvcViewModel createDetailsJobOfferViewModel)
        {
            if (ModelState.IsValid)
            {
                var responseFromCommand = await _jobOfferPropositionService.AddAsync(createDetailsJobOfferViewModel.JobOfferId, createDetailsJobOfferViewModel.Content);

                if (responseFromCommand.Success)
                {
                    ModelState.Clear();                  
                }
            }

            var jobOffer = await _jobOfferService.GetByIdAsync(createDetailsJobOfferViewModel.JobOfferId);

            var propositionMapped = Mapper.Map<List<JobOfferJobOfferPropositionMvcDto>>(jobOffer.Propositions);            

            return View(new CreateDetailsJobOfferPropositionMvcViewModel() { JobOfferId = createDetailsJobOfferViewModel.JobOfferId, Propositions = propositionMapped });
        }

        // GET: JobOfferSkill/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _jobOfferPropositionService.GetByIdAsync(id);

            var viewModel = Mapper.Map<UpdateJobOfferPropositionMvcViewModel>(response);

            return View(viewModel);
        }

        // POST: JobOfferSkill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content")] UpdateJobOfferPropositionMvcViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _jobOfferPropositionService.UpdateAsync(viewModel.Id, viewModel.Content);

            return RedirectToAction("Index", "JobOffer");
        }

        // GET: JobOfferSkill/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var responseFromQuery = await _jobOfferPropositionService.GetByIdAsync(id);

            var viewModel = Mapper.Map<DeleteJobOfferPropositionMvcViewModel>(responseFromQuery);

            return View(viewModel);
        }

        // POST: JobOfferSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _jobOfferPropositionService.DeleteAsync(id);

            return RedirectToAction("Index", "JobOffer");
        }
    }
}