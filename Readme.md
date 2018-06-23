
# Notes

It is always nice to see a github repo ReadMe file containing a demo page:
https://decisiontechbasketv1.azurewebsites.net/swagger/index.html

Just the bare minimum as the spec of the codetest requested.

Please see the following files for the bits you may be interested in:

<a href="https://github.com/janostorgyik-dt/DecisionTech/blob/master/DecisionTech.Basket/DomainServices/BasketService.cs" target="_blank">BasketService.cs</a> and <a href="https://github.com/janostorgyik-dt/DecisionTech/blob/master/DecisionTech.Basket.UnitTest/DomainService/BasketServiceTests.cs" target="_blank">BasketServiceTests.cs</a>

<a href="https://github.com/janostorgyik-dt/DecisionTech/blob/master/DecisionTech.Basket/DomainServices/BasketPricingService.cs" target="_blank">BasketPricingService.cs</a> and <a href="https://github.com/janostorgyik-dt/DecisionTech/blob/master/DecisionTech.Basket.UnitTest/DomainService/BasketPricingServiceTests.cs" target="_blank">BasketPricingServiceTests.cs</a>


Cheers






# Price calculation exercise

```
Create a customer basket that allows a customer to add products and provides a total cost of the
basket including applicable discounts.
```
## Available products

```
Product Cost
Butter £0.8
```
```
Milk £1.15
```
```
Bread £1.0
```
## Offers

- Buy 2 Butter and get a Bread at 50% off
- Buy 3 Milk and get the 4th milk for free

## Scenarios

- **Given** the basket has 1 bread, 1 butter and 1 milk **when** I total the basket **then** the total
    should be £2.95
- **Given** the basket has 2 butter and 2 bread **when** I total the basket **then** the total should be
    £3.10
- **Given** the basket has 4 milk **when** I total the basket **then** the total should be £3.45
- **Given** the basket has 2 butter, 1 bread and 8 milk **when** I total the basket **then** the total
    should be £9.00

```
The solution should be in C#.
```
## Pointers

```
While we are looking for a solution that shows a good understanding of the SOLID principles, object
oriented programming (or functional) and that displays a working knowledge of TDD (a git repository
will be appreciated!), please keep things simple- do not over-engineer anything, make sure your
code is manageable and easy to follow.
```
```
If you think your solution is too short, feel free to add some commentary to justify the decisions
you've taken, if possible by quoting books, blog posts or talks that have influenced you in the way
you've coded.
```
```
Above all, code as if you're coding for yourself.
```
