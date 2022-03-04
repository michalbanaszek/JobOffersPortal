using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Interfaces;
using WebApp.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.DeleteJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.UpdateJobOfferSkillMvc;

namespace WebApp.Controllers
{
    public class JobOfferSkillController : WebControllerBase
    {
        private readonly IJobOfferMvcService _jobOfferService;
        private readonly IJobOfferSkillMvcService _jobOfferSkillService;

        public JobOfferSkillController(IJobOfferSkillMvcService jobOfferPropositionService, IJobOfferMvcService jobOfferService)
        {
            _jobOfferSkillService = jobOfferPropositionService;
            _jobOfferService = jobOfferService;
        }

        // GET: JobOfferSkill/CreateShowDetails/5
        public async Task<IActionResult> CreateShowDetails(string id)
        {
            var response = await _jobOfferService.GetByIdAsync(id);

            var skillMapped = Mapper.Map<List<JobOfferJobOfferSkillMvcDto>>(response.Skills);

            var viewModel = new CreateDetailsJobOfferSkillMvcViewModel() { JobOfferId = id, Skills = skillMapped };

            return View(viewModel);
        }

        // POST: JobOfferSkill/CreateShowDetails/5
        [HttpPost]
        public async Task<IActionResult> CreateShowDetails(CreateDetailsJobOfferSkillMvcViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var responseFromCommand = await _jobOfferSkillService.AddAsync(viewModel.JobOfferId, viewModel.Content);

                if (responseFromCommand.Success)
                {
                    ModelState.Clear();
                }
            }

            var responseFromQuery = await _jobOfferService.GetByIdAsync(viewModel.JobOfferId);

            var skillMapped = Mapper.Map<List<JobOfferJobOfferSkillMvcDto>>(responseFromQuery.Skills);

            return View(new CreateDetailsJobOfferSkillMvcViewModel() { JobOfferId = viewModel.JobOfferId, Skills = skillMapped });
        }

        // GET: JobOfferSkill/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _jobOfferSkillService.GetByIdAsync(id);

            var viewModel = Mapper.Map<UpdateJobOfferSkillMvcViewModel>(response);

            return View(viewModel);
        }

        // POST: JobOfferSkill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content")] UpdateJobOfferSkillMvcViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _jobOfferSkillService.UpdateAsync(viewModel.Id, viewModel.Content);

            return RedirectToAction("Index", "JobOffer");
        }

        // GET: JobOfferSkill/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var responseFromQuery = await _jobOfferSkillService.GetByIdAsync(id);

            var viewModel = Mapper.Map<JobOfferSkillDeleteMvcViewModel>(responseFromQuery);

            return View(viewModel);
        }

        // POST: JobOfferSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _jobOfferSkillService.DeleteAsync(id);

            return RedirectToAction("Index", "JobOffer");
        }
    }
}
