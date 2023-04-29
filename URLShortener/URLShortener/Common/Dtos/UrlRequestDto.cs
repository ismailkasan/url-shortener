using FluentValidation;

namespace URLShortener.Common
{
    /// <summary>
    /// UrlRequestDto model for short link creation.
    /// </summary>
    public class UrlRequestDto
    {
        public UrlRequestDto(string longUrl, string customSegment)
        {
            LongUrl = longUrl;
            CustomSegment = customSegment;
        }

        /// <summary>
        /// long url from request
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Custom segment for custom url creation.
        /// </summary>
        public string CustomSegment { get; set; }

        /// <summary>
        /// Flag for auto generation short url or custom short url creation.
        /// </summary>
        public bool? IsCustomUrl { get; set; }
    }

    /// <summary>
    /// Validator for UrlRequestDto properties.
    /// </summary>
    public class UrlRequestDtoValidator : AbstractValidator<UrlRequestDto>
    {
        /// <summary>
        /// Constructor of UrlRequestDtoValidator
        /// </summary>
        public UrlRequestDtoValidator()
        {
            // long url must be not empt or null.
            RuleFor(x => x.LongUrl).NotEmpty().NotNull().WithMessage("Url must be not empty!");

            // Rules for custom short link creation.
            When(x => x.IsCustomUrl.HasValue && x.IsCustomUrl == true, () =>
            {
                RuleFor(x => x.CustomSegment).NotEmpty().NotNull().WithMessage("Segment must be not empty!");
                RuleFor(x => x.CustomSegment).MaximumLength(Constant.ShortLinkSegmentLength).WithMessage($"Custom url segment maximum length can be {Constant.ShortLinkSegmentLength}!");
            });
        }
    }
}
