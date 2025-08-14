# Peppol_Bis
- Peppol BIS Billing 3.0 là một tiêu chuẩn hóa đơn điện tử được sử dụng rộng rãi trong khuôn khổ Peppol, đặc biệt là ở châu Âu. Nó quy định cấu trúc và nội dung của hóa đơn điện tử để đảm bảo tính tương thích và khả năng xử lý tự động trên các hệ thống khác nhau, bất kể phần mềm được sử dụng.
- Peppol (Pan-European Public Procurement Online):
Là một hệ thống mạng lưới và quy trình trao đổi dữ liệu, được thiết kế để hỗ trợ các doanh nghiệp và khu vực công trong việc trao đổi tài liệu điện tử, bao gồm cả hóa đơn. 
- BIS (Business Interoperability Specification):
Là một tập hợp các quy tắc và thông số kỹ thuật được sử dụng trong Peppol để đảm bảo khả năng tương tác giữa các hệ thống khác nhau. 
- Billing 3.0:
Đề cập đến phiên bản cụ thể của đặc tả hóa đơn điện tử trong khuôn khổ Peppol. Phiên bản 3.0 là phiên bản mới nhất và được sử dụng rộng rãi. 
- Example: 
// follow theo https://docs.peppol.eu/poacc/billing/3.0/syntax/ubl-invoice/tree/
<Invoice xmlns="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
         xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2" 
         xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"> //hoá đơn theo tiêu chuẩn UBL
         //cac : Common Aggregate Component : Bên trong còn nhiều trường con
         //cbc : Common Basic Components. : Chứa những trường lẻ
    <cbc:UBLVersionID>2.1</cbc:UBLVersionID>  //Phiên bản UBL đang sử dụng
    <cbc:CustomizationID>urn:cen.eu:en16931:2017</cbc:CustomizationID> 
    <cbc:ProfileID>urn:fdc:peppol.eu:2017:poacc:billing:01:1.0</cbc:ProfileID>
    <cbc:ID>INV-1001</cbc:ID> // ID hoá đơn được phát hành
    <cbc:IssueDate>2025-08-13</cbc:IssueDate> // ngày phát hành Theo format YYYY-MM-DD
    <cac:AccountingSupplierParty>
        <cac:Party> 
            <cac:PartyName>
                <cbc:Name>CMC Global</cbc:Name>
            </cac:PartyName>
            <cac:PostalAddress>
                <cbc:StreetName>265 CG</cbc:StreetName>
                <cbc:CityName>Hanoi</cbc:CityName>
                <cbc:CountrySubentity>HN</cbc:CountrySubentity>
                <cac:Country>
                    <cbc:IdentificationCode>VN</cbc:IdentificationCode>
                </cac:Country>
            </cac:PostalAddress>
        </cac:Party>
    </cac:AccountingSupplierParty>
    <cac:AccountingCustomerParty>
        <cac:Party>
            <cac:PartyName>
                <cbc:Name>Fanabie</cbc:Name>
                <cac:PostalAddress>
                <cbc:StreetName>Le Van Luong</cbc:StreetName>
                <cbc:CityName>Hanoi</cbc:CityName>
                <cbc:CountrySubentity>HN</cbc:CountrySubentity>
                <cac:Country>
                    <cbc:IdentificationCode>VN</cbc:IdentificationCode>
                </cac:Country>
            </cac:PostalAddress>
            </cac:PartyName>
        </cac:Party>
    </cac:AccountingCustomerParty>
    <cac:InvoiceLine>
        <cbc:ID>1</cbc:ID>
        <cbc:InvoicedQuantity unitCode="EA">5</cbc:InvoicedQuantity>
        <cbc:LineExtensionAmount currencyID="VND">2500000</cbc:LineExtensionAmount>
        <cac:Item>
            <cbc:Description>Premium Software License</cbc:Description>
        </cac:Item>
        <cac:Price>
            <cbc:PriceAmount currencyID="VND">500000</cbc:PriceAmount>
        </cac:Price>
    </cac:InvoiceLine>
    <cac:LegalMonetaryTotal>
        <cbc:PayableAmount currencyID="VND">2500000</cbc:PayableAmount>
    </cac:LegalMonetaryTotal>
</Invoice>

Để test
{
  "xmlString": "<Invoice xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\"> <cbc:UBLVersionID>2.1</cbc:UBLVersionID> <cbc:CustomizationID>urn:cen.eu:en16931:2017</cbc:CustomizationID> <cbc:ProfileID>urn:fdc:peppol.eu:2017:poacc:billing:01:1.0</cbc:ProfileID> <cbc:ID>INV-1001</cbc:ID> <cbc:IssueDate>2025-08-13</cbc:IssueDate> <cac:AccountingSupplierParty> <cac:Party> <cac:PartyName> <cbc:Name>CMC Global</cbc:Name> </cac:PartyName> <cac:PostalAddress> <cbc:StreetName>265 CG</cbc:StreetName> <cbc:CityName>Hanoi</cbc:CityName> <cbc:CountrySubentity>HN</cbc:CountrySubentity> <cac:Country> <cbc:IdentificationCode>VN</cbc:IdentificationCode> </cac:Country> </cac:PostalAddress> </cac:Party> </cac:AccountingSupplierParty> <cac:AccountingCustomerParty> <cac:Party> <cac:PartyName> <cbc:Name>Fanabie</cbc:Name> <cac:PostalAddress> <cbc:StreetName>Le Van Luong</cbc:StreetName> <cbc:CityName>Hanoi</cbc:CityName> <cbc:CountrySubentity>HN</cbc:CountrySubentity> <cac:Country> <cbc:IdentificationCode>VN</cbc:IdentificationCode> </cac:Country> </cac:PostalAddress> </cac:PartyName> </cac:Party> </cac:AccountingCustomerParty> <cac:InvoiceLine> <cbc:ID>1</cbc:ID> <cbc:InvoicedQuantity unitCode=\"EA\">5</cbc:InvoicedQuantity> <cbc:LineExtensionAmount currencyID=\"VND\">2500000</cbc:LineExtensionAmount> <cac:Item> <cbc:Description>Premium Software License</cbc:Description> </cac:Item> <cac:Price> <cbc:PriceAmount currencyID=\"VND\">500000</cbc:PriceAmount> </cac:Price> </cac:InvoiceLine> <cac:LegalMonetaryTotal> <cbc:PayableAmount currencyID=\"VND\">2500000</cbc:PayableAmount> </cac:LegalMonetaryTotal> </Invoice>"
}