using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Domain.Property;
public class AssetHistory : AuditableEntity, IAggregateRoot
{
    public DefaultIdType AssetId { get; private set; }
    public virtual Asset Asset { get; private set; } = default!;

    public DefaultIdType? PreviousQualityStatusId { get; private set; }
    public virtual AssetStatus? PreviousQualityStatus { get; private set; }

    public DefaultIdType QualityStatusId { get; private set; }
    public virtual AssetStatus QualityStatus { get; private set; } = default!;

    public DefaultIdType? PreviousUsingStatusId { get; private set; }
    public virtual AssetStatus? PreviousUsingStatus { get; private set; }

    public DefaultIdType UsingStatusId { get; private set; }
    public virtual AssetStatus UsingStatus { get; private set; } = default!;

    public DefaultIdType? EmployeeId { get; private set; }
    public virtual Employee? Employee { get; private set; }

    public string? DoccumentPath { get; private set; }
    public string? DoccumentLink { get; private set; }

    public string? Note { get; private set; }

    public AssetHistory(
        DefaultIdType assetId,
        DefaultIdType? previousQualityStatusId,
        DefaultIdType qualityStatusId,
        DefaultIdType? previousUsingStatusId,
        DefaultIdType usingStatusId,
        DefaultIdType? employeeId,
        string? doccumentPath,
        string? doccumentLink,
        string? note)
    {
        AssetId = assetId;
        PreviousQualityStatusId = (previousQualityStatusId == DefaultIdType.Empty) ? null : previousQualityStatusId;
        QualityStatusId = qualityStatusId;
        PreviousUsingStatusId = (previousUsingStatusId == DefaultIdType.Empty) ? null : previousUsingStatusId;
        UsingStatusId = usingStatusId;
        EmployeeId = (employeeId == DefaultIdType.Empty) ? null : employeeId;
        DoccumentPath = doccumentPath ?? string.Empty;
        DoccumentLink = doccumentLink ?? string.Empty;
        Note = note ?? string.Empty;
    }

    public AssetHistory()
    : this(
    DefaultIdType.Empty,
    null,
    DefaultIdType.Empty,
    null,
    DefaultIdType.Empty,
    null,
    string.Empty,
    string.Empty,
    string.Empty)
    {
    }

    public AssetHistory Update(
    DefaultIdType? assetId,
    DefaultIdType? previousQualityStatusId,
    DefaultIdType? qualityStatusId,
    DefaultIdType? previousUsingStatusId,
    DefaultIdType? usingStatusId,
    DefaultIdType? employeeId,
    string? doccumentPath,
    string? doccumentLink,
    string? note)
    {
        // Do not allow change assetId
        // if (assetId.HasValue && assetId.Value != DefaultIdType.Empty && !AssetId.Equals(assetId.Value)) AssetId = assetId.Value;

        if (previousQualityStatusId.HasValue && previousQualityStatusId.Value != DefaultIdType.Empty && !PreviousQualityStatusId.Equals(previousQualityStatusId.Value)) PreviousQualityStatusId = previousQualityStatusId.Value;
        if (qualityStatusId.HasValue && qualityStatusId.Value != DefaultIdType.Empty && !QualityStatusId.Equals(qualityStatusId.Value)) QualityStatusId = qualityStatusId.Value;
        if (previousUsingStatusId.HasValue && previousUsingStatusId.Value != DefaultIdType.Empty && !PreviousUsingStatusId.Equals(previousUsingStatusId.Value)) PreviousUsingStatusId = previousUsingStatusId.Value;
        if (usingStatusId.HasValue && usingStatusId.Value != DefaultIdType.Empty && !UsingStatusId.Equals(usingStatusId.Value)) UsingStatusId = usingStatusId.Value;
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;

        if (doccumentPath is not null && DoccumentPath?.Equals(doccumentPath) is not true) DoccumentPath = doccumentPath;
        if (doccumentLink is not null && DoccumentLink?.Equals(doccumentLink) is not true) DoccumentLink = doccumentLink;
        if (note is not null && Note?.Equals(note) is not true) Note = note;

        return this;
    }

    public AssetHistory ClearDoccumentPath()
    {
        DoccumentPath = string.Empty;
        return this;
    }
}