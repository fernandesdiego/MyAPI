
# MyAPI
Sample ASP.NET Core REST WebAPI
This repo contains an API project divided in 3 layers

|Name		   |Type	 |
|--------------|---------|
|MyAPI.API	   |Web		 |
|MyAPI.Business|Class Lib|
|MyAPI.Data	   |Class Lib|
## Considerations
This was built as a study to understand concepts of decoupled code, dependency injection, layering, validations, authentication/authorization, JWT.

## Summary
### MyAPI.API
The API was built using the MVC pattern and is documented with Swagger. 
It handles the the authentication and authorization using JWT.
### MyAPI.Business
This layer handles the business rules using `FluentValidation`, exposes services for the API to handle the models and interacts with the Data layer.
### MyAPI.Data
This layer implements the interfaces defined in the Business layers and provides the concrete classes for the business to consume.

## Implementation Details
#### Repositories
The repository is a generic abstract class that implements the methods defined in the Business layer interface to handle the objects in the system.

>```c#
>public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()

And then you implement the generic methods as follows

>```c#
>public virtual async Task<TEntity> GetByIdAsync(Guid id)
>{
>	return await _dbset.FindAsync(id);
>}

Now you don't need to implement the basic methods on all of your concrete repository classes. Use inheritance and write specific only methods.

>```c#
>public class AddressRepository : Repository<Address>, IAddressRepository


In the example above you have all the methods provided by the abstract class + you can implement the class specific methods and methods from the interface as the Business layer requires.

#### Business
Here is where we define the interfaces, models, notifiers, validations and expose the services to the API.
The validations are made using the `AbstractValidator<T>`
Ex:
>```c#
>public class AddressValidation : AbstractValidator<Address>
>{
>	public AddressValidation()
>     {
>          RuleFor(x => x.Street)
>               .NotEmpty().WithMessage("The field {PropertyName} is required.")
>               .Length(2, 100).WithMessage("The field {PropertyName} must be between {MinLenght} and {MaxLenght} characters.");
>          }
>     }
>}
Now the business services can use this validation as follows:

>```c#
>public async Task UpdateAddress(Address address)
>{
>     if (!ExecuteValidation(new AddressValidation(), address))
>          return;
>     await _addressRepository.Update(address);
>}

#### WebAPI
The WebAPI is responsible for consuming the services from the Business layer and expose the functionalities to the user after registration.

To register the users we use the Identity with the default IdentityUser. The authentication and authorization is started with the Identity SignInManager and then a JWT is made with all user data such as their roles and claims.
>```c#
> [HttpPost("login")]
>public async Task<ActionResult> Login (LoginUserViewModel loginUser)
>{
>     //[...]
>     var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
>     if(result.Succeeded)
>     {
>          return CustomResponse(await GenerateJwtAsync(loginUser.Email));
>     }
>     //[...]
>}

``Automapper`` also was used to map the ViewModels to the Models
>```c#
>//This way we can map back and forth
>CreateMap<Supplier, SupplierViewModel>().ReverseMap();

All the endpoints return a ``CustomResponse`` object that handles the response based on the operation's results and the ``Notifier``. The standard response follows this format.

>```c#
>return Ok(new
>{
>     success = true,
>     data = result //A JSON object containing the return of a request
>});

![DESCRIPTION](https://img001.prntscr.com/file/img001/UvyekEKiQaG0zwvgm10EFw.png)
