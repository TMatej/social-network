﻿using BusinessLayer.DTOs.User;
using BusinessLayer.Facades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BusinessLayer.DTOs.Group;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupFacade groupFacade;

        public GroupController(IGroupFacade groupFacade)
        {
            this.groupFacade = groupFacade;
        }
        
        [HttpPost]
        public IActionResult CreateGroup(GroupCreateDTO groupCreateDTO)
        {
            groupFacade.CreateGroup(groupCreateDTO);
            return Ok();
        }
        
        [HttpGet]
        public IActionResult GetGroup(int groupId)
        {
            var group = groupFacade.GetGroup(groupId);
            return group == null ? NotFound() : Ok(group);
        }
        
        [HttpPut]
        public IActionResult UpdateGroup(GroupRepresentDTO groupRepresentDTO)
        {
            groupFacade.UpdateGroup(groupRepresentDTO);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteGroup(GroupRepresentDTO groupRepresentDTO)
        {
            groupFacade.DeleteGroup(groupRepresentDTO);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMembership(GroupMembershipDTO groupMembershipDTO)
        {
            var success = groupFacade.RemoveFromGroup(groupMembershipDTO);
            return success ? Ok() : NotFound();
        }

        [HttpPost]
        public IActionResult AddMembership(GroupMembershipDTO groupMembershipDTO)
        {
            groupFacade.AddToGroup(groupMembershipDTO);
            return Ok();
        }
    }
}