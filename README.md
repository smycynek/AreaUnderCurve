# AreaUnderCurve
.NET Core library and application to calculate area under a curve

* Version 0.1.0
* Copyright 2017 Steven Mycynek
* Supports 
    * simpson, trapezoid, and midpoint algorithms, 
    * n-degree single variable polynomials, including fractional exponents,
    * variable step size

`USAGE =  -/p|/polynomial {DegreeN1:CoefficientM1, DegreeN2:CoefficientM2, ...}...`
`/l|/lowerBound <lower bound> -/|/upperBound <upper bound> -/s|/stepSize <step size>` 
`/a|/algorithm <Simpson | Trapezoid | Midpoint>`

* I did a python project just for fun (https://github.com/smycynek/area_under_curve), so I decided to make a .NET Core version.

* Try a simple function you can integrate by hand easily, like `f(x) = x^3` from `[0-10]`, and compare that to how accurate the midpoint, trapezoid, and simpson approximations are with various steps sizes.


## Examples:

`dotnet AreaUnderCurve.App.dll /polynomial {3:1} /lowerBound 0 /upperBound 10 /stepSize .1 /algorithm Simpson`



Also try out `AreaUnderCurve.Tests` and `AreaUnderCurve.Demo`.
