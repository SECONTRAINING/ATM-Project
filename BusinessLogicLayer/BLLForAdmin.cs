using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;


namespace BusinessLogicLayer
{
    public class BLLForAdmin
    {
        private readonly IMapper _mapper;
        private readonly Repository<LoginCredential> _repository;

        public BLLForAdmin(Repository<LoginCredential> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<LoginCredentialView> GetLogins()
        {
            try
            {
                // Retrieve all Logins from the repository
                var Logins = _repository.GetAll();

                // Map the Login entities to LoginCredentialView objects using AutoMapper

                var LoginCredentialViews = _mapper.Map<List<LoginCredentialView>>(Logins);


                return LoginCredentialViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public int SaveLogin(LoginCredentialView loginCredential)
        //{
        //    try
        //    {
        //        // Hash the password
        //        var passwordHasher = new PasswordHasher();
        //        var (hashedPassword, salt) = passwordHasher.HashPassword(loginCredential.Password);

        //        // Map the LoginCredentialView to a Login entity
        //        var data = _mapper.Map<LoginCredential>(loginCredential);

        //        // Set the hashed password and salt in the entity
        //        data.Password = hashedPassword;
        //        data.Salt = salt;

        //        // Update the Login in the database
        //        _repository.Insert(data);

        //        // Save changes to the database
        //        _repository.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions or log them as needed
        //        throw new Exception(ex.Message);
        //    }
        //    return 1;
        //}


        public int SaveLogin(LoginCredentialView loginCredential)
        {
            try
            {
                // Hash the user's password using BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginCredential.Password);

                Random randomAccountNo = new Random();


                // Map the LoginCredentialView to a Login entity
                var data = _mapper.Map<LoginCredential>(loginCredential);

                // Replace the password with the hashed password
                data.Password = hashedPassword;

                // Update the Login in the database
                _repository.Insert(data);

                // Save changes to the database
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return 1;
        }



        public int UpdateLogin(int LoginId, LoginCredentialView loginCredential)
        {
            try
            {
                // Retrieve the Login by ID from the repository
                var existingLogin = _repository.GetById(LoginId);

                if (existingLogin == null)
                {
                    // Handle the case where the Login with the specified ID was not found
                    throw new Exception($"Login with ID {LoginId} not found.");
                }

                // Map the updated data from LoginCredentialView to the existingLogin entity
                existingLogin.UserID = LoginId;
                _mapper.Map(loginCredential, existingLogin);
                existingLogin.UserID = LoginId;
                // Update the Login in the repository
                _repository.Update(LoginId, existingLogin);

                _repository.SaveChanges();
                return 1;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LoginCredentialView GetLoginById(int Id)
        {
            try
            {
                // Retrieve the Login by ID from the repository
                var login = _repository.GetById(Id);

                // Map the Login entity to a LoginCredentialView object using AutoMapper
                var LoginCredentialView = _mapper.Map<LoginCredentialView>(login);

                return LoginCredentialView;
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception(ex.Message);
            }
        }

        public int DeleteLogin(int LoginId)
        {
            try
            {
                _repository.Delete(LoginId);

                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return 1;
        }
    }
}
