using ErrorOr;

namespace SavePlan.API.Common;

public class Problem
{
    public static IResult Errors(List<Error> errors)
    {
        if (errors.Count == 0)
        {
            return Results.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return GetProblem(errors[0]);
    }

    private static IResult GetProblem(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }

    private static IResult ValidationProblem(List<Error> errors)
    {
        Dictionary<string, string[]> modelStateDictionary = new();

        foreach (var error in errors)
        {
            modelStateDictionary.Add(error.Code, new string[] { error.Description });
        }

        return Results.ValidationProblem(modelStateDictionary);
    }
}
