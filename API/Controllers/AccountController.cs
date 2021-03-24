using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class AccountController : BaseApiController
   {
      private readonly ITokenService _tokenService;
      // Initilze field from parameter
      private readonly IMapper _mapper;
      // Initilize field from parameter
      private readonly UserManager<AppUser> _userManager;
      private readonly SignInManager<AppUser> _signInManager;
      public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
      {
         _signInManager = signInManager;
         _userManager = userManager;
         _mapper = mapper;
         _tokenService = tokenService;
      }

      [HttpPost("register")]
      public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
      {
         if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken.");

         var user = _mapper.Map<AppUser>(registerDTO);

         // await Seed.SeedUsers(_context);

         // using var hmac = new HMACSHA512();


         user.UserName = registerDTO.Username.ToLower();

         var result = await _userManager.CreateAsync(user, registerDTO.Password);

         if (!result.Succeeded) return BadRequest(result.Errors);
         // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
         // user.PasswordSalt = hmac.Key;

         // _context.Users.Add(user);
         // await _context.SaveChangesAsync();

         var roleResult = await _userManager.AddToRoleAsync(user, "Member");
         if (!roleResult.Succeeded) return BadRequest(result.Errors);

         return new UserDTO
         {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender
         };
      }

      [HttpPost("login")]
      public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
      {
         var user = await _userManager.Users
           .Include(p => p.Photos)
           .SingleOrDefaultAsync(u => u.UserName == loginDTO.Username.ToLower());

         if (user == null) return Unauthorized("Invalid username");

         var result = await _signInManager
               .CheckPasswordSignInAsync(user, loginDTO.Password, false);

         if (!result.Succeeded) return Unauthorized();

         // using var hmac = new HMACSHA512(user.PasswordSalt);

         // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

         // for (int i = 0; i < computedHash.Length; i++)
         // {
         //    if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
         // }

         return new UserDTO
         {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
         };
      }

      private async Task<bool> UserExists(string username)
      {
         return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
      }
   }
}
