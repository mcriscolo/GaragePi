using Microsoft.AspNetCore.Mvc;

namespace GaragePi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GarageController : ControllerBase
    {
        private readonly ILogger<GarageController> _logger;
        private GarageDoorService _svc;

        public GarageController(ILogger<GarageController> logger, GarageDoorService svc)
        {
            _logger = logger;
            _svc = svc;
        }

        [HttpGet]
        public ActionResult<List<DoorStatus>> Get()
        {
            List<DoorStatus> result = new List<DoorStatus>();   

            foreach (GarageDoor g in _svc.doors)
            {
                // refresh state
                g.getState();

                DoorStatus status = new DoorStatus();
                status.id = g.config.id;
                status.name = g.config.name;
                switch (g.LastKnownState)
                {
                    case DoorState.Open:
                        status.display_state = "Open";
                        break;
                    case DoorState.Closed:
                        status.display_state = "Closed";
                        break ;
                    case DoorState.Opening_Closing:
                        status.display_state = "Opening/Closing";
                        break;
                }
                result.Add(status);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("{id}")]
        public ActionResult triggerDoor(int id)
        {
            try
            {
                GarageDoor g = _svc.doors.Find(x => x.config.id == id);
                g.trigger();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}