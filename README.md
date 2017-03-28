# AreaUnderCurve
.NET Standard 1.3 / 4.5.1 library and application to calculate area under a curve

* Version 0.3.0
* Copyright 2017 Steven Mycynek
* https://www.nuget.org/packages/AreaUnderCurve.Core

* Supports 
    * Simpson, Trapezoid, Midpoint, and Romberg* algorithms, 
    * N-degree single variable polynomials, including fractional exponents,
    * Variable step size

`USAGE =  dotnet AreaUnderCurve.App.dll /polynomial {DegreeN1:CoefficientM1, DegreeN2:CoefficientM2, ...}...`
`/lowerBound <lower bound> /upperBound <upper bound> /stepSize <step size>` 
`/algorithm <Simpson | Trapezoid | Midpoint RombergNM>`

* I did a python project just for fun (https://github.com/smycynek/area_under_curve), so I decided to make a .NET Core version.

* Try a simple function you can integrate by hand easily, like `f(x) = x^3` from `[0-10]`, and compare that to how accurate the midpoint, trapezoid, and simpson approximations are with various steps sizes.


## Examples:
### Command-Line

`dotnet AreaUnderCurve.App.dll /polynomial {3:1} /lowerBound 0 /upperBound 10 /stepSize .1 /algorithm Simpson`

`AreaUnderCurve.App.exe /polynomial {3:1} /lowerBound 0 /upperBound 10 /stepSize .1 /algorithm Simpson`

### C#

`var simpson = Algorithms.GetAlgorithm("Simpson");`

`var boundsSimple1 = new Bounds(0, 10, .1);`

`var polySimpleCubic = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [3] = 1 });`

`AreaUnderCurve.Core.AreaUnderCurve.Calculate(polySimpleCubic, boundsSimple1, midpoint);`


### *Romberg's method

* Note that the "Romberg" implementation (https://en.wikipedia.org/wiki/Romberg's_method) is fairly rough and unoptimized, and it requires parameters `n` and `m`
`(n >=m >=1)` to determine the iterations and subdivisions.   `n` and `m` can be set at the command line as a suffix, like this:

`dotnet AreaUnderCurve.App.dll /polynomial {4:1} /lowerBound 0 /upperBound 5 /stepSize 5 /algorithm Romberg65`

If no suffix is set,`(4,3)` is used as a default.

* Also note that since the Romberg method already subdivides a curve, selecting a small step size parameter separately is not strictly necessary.  A step size equal to the the total length of the bound (see example above) will often yield reasonable results.

## More examples
Try out `AreaUnderCurve.Tests` and `AreaUnderCurve.Demo`.
