﻿using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository) 
        {
            _stateRepository = stateRepository;    
        }

        #region GetAllStates

        [HttpGet]
        public IActionResult Index()
        {
            var states = _stateRepository.GetAllStates();

            return Ok(states);
        }
        #endregion

        #region InsertState
        [HttpPost]
        public IActionResult InsertState(StateModel smodel)
        {
            bool Reflected = _stateRepository.InsertState(smodel);
            return Ok(Reflected);
        }
        #endregion

        #region UpdateState
        [HttpPut]
        public IActionResult UpdateState(StateModel stateModel)
        {
            bool Reflected = _stateRepository.UpdateState(stateModel);
            return Ok(Reflected);
        }
        #endregion

        #region DeleteState
        [HttpDelete("{StateID:int}")]
        public IActionResult DeleteState(int StateID)
        {
            bool Reflected = _stateRepository.DeleteState(StateID);
            return Ok(Reflected);
        }
        #endregion

        #region GetStateByPK
        [HttpGet("{StateID:int}")]
        public IActionResult GetStateByPK(int StateID)
        {
            StateModel smodel = _stateRepository.GetStateByPK(StateID);
            return Ok(smodel);
        }
        #endregion
    }
}
