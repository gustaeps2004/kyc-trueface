namespace KYC.TrueFace.Core.Domain.Enums;

public enum FaceComparisonOutcome
{
    /// <summary>The faces match above the configured similarity threshold.</summary>
    Matched = 1,

    /// <summary>The provider compared both images and the faces are not the same person.</summary>
    NotMatched = 2,

    /// <summary>The provider could not decide (no face found, unreadable image) - needs a human.</summary>
    Inconclusive = 3
}
