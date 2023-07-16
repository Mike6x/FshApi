using FSH.WebApi.Domain.Settings;
using Newtonsoft.Json;

namespace FSH.WebApi.Domain.Integration;

public class ApiSerial : AuditableEntity, IAggregateRoot
{
    [JsonProperty(PropertyName = "serial_number")]
    public string ItemSerial { get; set; } = default!;

    [JsonProperty(PropertyName = "item_number")]
    public string ItemCode { get; set; } = default!;

    [JsonProperty(PropertyName = "Item_Name")]
    public string ItemName { get; set; } = default!;

    [JsonProperty(PropertyName = "Item_Class")]
    public string ItemClass { get; set; } = default!;

    [JsonProperty(PropertyName = "Item_Brand")]
    public string ItemBrand { get; set; } = default!;

    [JsonProperty(PropertyName = "purchase_order_no")]
    public string PoNumber { get; set; } = default!;

    [JsonProperty(PropertyName = "purchase_order_status")]
    public string PoStatus { get; set; } = default!;

    [JsonProperty(PropertyName = "created_date")]
    public DateTime PoCreatedDate { get; set; } = default!;

    [JsonProperty(PropertyName = "modified_date")]
    public DateTime PoModifiedDate { get; set; } = default!;

    [JsonProperty(PropertyName = "po_process_status")]
    public string PoProcessStatus { get; set; } = default!;

    [JsonProperty(PropertyName = "custom_tara_status_sys")]
    public string CustomStatusSys { get; set; } = default!;

    [JsonProperty(PropertyName = "custom_tara_status_ibsm")]
    public string CustomStatusIbsm { get; set; } = default!;

    /// <summary>
    /// Success.
    /// standby.
    /// failed
    /// Dulicated.
    /// Existed.
    /// </summary>
    public string? ImportStatus { get; set; }
    public DefaultIdType? CronJobId { get; set; }
    public virtual CronJob? CronJob { get; set; }

    public ApiSerial(
                string itemSerial,
                string itemCode,
                string itemName,
                string itemClass,
                string itemBrand,
                string poNumber,
                string poStatus,
                DateTime poCreatedDate,
                DateTime poModifiedDate,
                string poProcessStatus,
                string customStatusSys,
                string customStatusIbsm,
                string importStatus,
                DefaultIdType? cronJobId)
    {
        ItemSerial = itemSerial;
        ItemCode = itemCode;
        ItemName = itemName;
        ItemClass = itemClass;
        ItemBrand = itemBrand;
        PoNumber = poNumber;
        PoStatus = poStatus;
        PoCreatedDate = poCreatedDate;
        PoModifiedDate = poModifiedDate;
        PoProcessStatus = poProcessStatus;
        CustomStatusSys = customStatusSys;
        CustomStatusIbsm = customStatusIbsm;
        ImportStatus = importStatus;
        CustomStatusIbsm = customStatusIbsm;
        ImportStatus = importStatus;
        CronJobId = (cronJobId == DefaultIdType.Empty) ? null : cronJobId;
    }

    public ApiSerial()
        : this(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            DateTime.UtcNow,
            DateTime.UtcNow,
            string.Empty,
            string.Empty,
            string.Empty,
            "Standby",
            null)
    {
    }

    public ApiSerial Update(
                string? itemSerial,
                string? itemCode,
                string? itemName,
                string? itemClass,
                string? itemBrand,
                string? poNumber,
                string? poStatus,
                DateTime? poCreatedDate,
                DateTime? poModifiedDate,
                string? poProcessStatus,
                string? customStatusSys,
                string? customStatusIbsm,
                string? importStatus,
                DefaultIdType? cronJobId)
    {
        if (itemSerial is not null && ItemSerial?.Equals(itemSerial) is not true) ItemSerial = itemSerial;
        if (itemCode is not null && ItemCode?.Equals(itemCode) is not true) ItemCode = itemCode;
        if (itemName is not null && ItemName?.Equals(itemName) is not true) ItemName = itemName;
        if (itemClass is not null && ItemClass?.Equals(itemClass) is not true) ItemClass = itemClass;
        if (itemBrand is not null && ItemBrand?.Equals(itemBrand) is not true) ItemBrand = itemBrand;

        if (poNumber is not null && PoNumber?.Equals(poNumber) is not true) PoNumber = poNumber;
        if (poStatus is not null && PoStatus?.Equals(poStatus) is not true) PoStatus = poStatus;
        if (poCreatedDate is not null && !PoCreatedDate.Equals(poCreatedDate)) PoCreatedDate = (DateTime)poCreatedDate;
        if (poModifiedDate is not null && !PoModifiedDate.Equals(poModifiedDate)) PoModifiedDate = (DateTime)poModifiedDate;

        if (poProcessStatus is not null && PoProcessStatus?.Equals(poProcessStatus) is not true) PoProcessStatus = poProcessStatus;
        if (customStatusSys is not null && CustomStatusSys?.Equals(customStatusSys) is not true) CustomStatusSys = customStatusSys;
        if (customStatusIbsm is not null && CustomStatusIbsm?.Equals(customStatusIbsm) is not true) CustomStatusIbsm = customStatusIbsm;

        if (importStatus is not null && ImportStatus?.Equals(importStatus) is not true) ImportStatus = importStatus;
        if (cronJobId.HasValue && cronJobId.Value != DefaultIdType.Empty && !CronJobId.Equals(cronJobId.Value)) CronJobId = cronJobId.Value;

        return this;
    }
}