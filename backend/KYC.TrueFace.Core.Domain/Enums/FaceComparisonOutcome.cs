namespace KYC.TrueFace.Core.Domain.Enums;

public enum FaceComparisonOutcome
{
    Matched = 1,

    /// <summary>The provider compared both images and the faces are not the same person.</summary>
    NotMatched = 2,

    /// <summary>The provider could not decide (no face found, unreadable image) - needs a human.</summary>
    Inconclusive = 3,

    /// <summary>Similarity fell in the uncertainty band - a human has to confirm the decision.</summary>
    ReviewRequired = 4
}
