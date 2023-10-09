using AutoMapper;
using DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class BLLForCustomerAccount
    {
        private readonly IMapper _mapper;
        private readonly Repository<CustomerAccount> _repository;

        public BLLForCustomerAccount(Repository<CustomerAccount> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<CustomerAccountView> GetCustomerAccounts()
        {
            try
            {
                // Retrieve all CustomerAccounts from the repository
                var CustomerAccounts = _repository.GetAll();

                // Map the CustomerAccount entities to CustomerAccountView objects using AutoMapper

                var CustomerAccountViews = _mapper.Map<List<CustomerAccountView>>(CustomerAccounts);


                return CustomerAccountViews;
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }
        }

        public int SaveCustomerAccount(CustomerAccountView CustomerAccountView)
        {
            try
            {
                // Map the CustomerAccountView to a CustomerAccount entity
                var data = _mapper.Map<CustomerAccount>(CustomerAccountView);

                // Update the CustomerAccount in the database
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

        public CustomerAccountView GetCustomerAccountById(int CustomerAccountId)
        {
            try
            {
                // Retrieve the CustomerAccount by ID from the repository
                var CustomerAccount = _repository.GetById(CustomerAccountId);

                // Map the CustomerAccount entity to a CustomerAccountView object using AutoMapper
                var CustomerAccountView = _mapper.Map<CustomerAccountView>(CustomerAccount);

                return CustomerAccountView;
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception(ex.Message);
            }
        }

        public int UpdateCustomerAccount(int CustomerAccountId, CustomerAccountView CustomerAccountView)
        {
            try
            {
                // Retrieve the CustomerAccount by ID from the repository
                var existingCustomerAccount = _repository.GetById(CustomerAccountId);

                if (existingCustomerAccount == null)
                {
                    // Handle the case where the CustomerAccount with the specified ID was not found
                    throw new Exception($"CustomerAccount with ID {CustomerAccountId} not found.");
                }

                // Map the updated data from CustomerAccountView to the existingCustomerAccount entity
                _mapper.Map(CustomerAccountView, existingCustomerAccount);

                // Update the CustomerAccount in the repository
                _repository.Update(CustomerAccountId, existingCustomerAccount);

                // Save changes to the repository
                _repository.SaveChanges();
                return 1;
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception(ex.Message);
            }
        }

        public int DeleteCustomerAccount(int CustomerAccountId)
        {
            try
            {
                // Delete the CustomerAccount by ID from the repository
                _repository.Delete(CustomerAccountId);



                // Save changes to the repository
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as needed
                throw new Exception(ex.Message);
            }
            return 1;
        }




    }
}
