using Domain.Base;

namespace Domain.Entities;

public class Address : Entity
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        ValidateEntity();
    }
    
    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Street, "A rua não pode ser vazia");
        AssertionConcern.AssertArgumentNotEmpty(City, "A cidade não pode ser vazia");
        AssertionConcern.AssertArgumentNotEmpty(State, "O estado não pode ser vazio");
        AssertionConcern.AssertArgumentNotEmpty(ZipCode, "O código postal não pode ser vazio");
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}";
    }
}