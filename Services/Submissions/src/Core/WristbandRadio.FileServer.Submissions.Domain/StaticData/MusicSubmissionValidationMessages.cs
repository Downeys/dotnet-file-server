namespace WristbandRadio.FileServer.Submissions.Domain.StaticData;

public static class MusicSubmissionValidationMessages
{
    public const string ContactNameInvalid = "Invalid contact name. Names can only include alphabetic letters, spaces, commas, periods, apostrophes, and dashes. Anything else will cause this submission to fail.";
    public const string ContactEmailInvalid = "Invalid email. Please ensure the email is accurate and formed properly.";
    public const string ContactPhoneInvalid = "Invalid phone number. Please ensure the phone number is accurate and formed properly.";
    public const string MissingAttestation = "Missing ownership attestation. Please attest to the ownership of the submission.";
    public const string MissingMusicFile = "Missing song file. Please provide at least one song file with your submission.";
    public const string MissingImageFile = "Missing image file. Please provide at least one album art image with your submission.";
}
