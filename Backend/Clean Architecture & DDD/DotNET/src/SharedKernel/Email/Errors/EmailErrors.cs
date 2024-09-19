using ErrorOr;

namespace SharedKernel.Email.Errors;

public static class EmailErrors
{
    public static readonly Error SubjectMissing = Error.Validation(
        code: "EmailData.SubjectMissing",
        description: "EmailData is missing a subject.");

    public static readonly Error ContentMissing = Error.Validation(
        code: "EmailData.ContentMissing",
        description: "EmailData is missing either html content or text content.");
}