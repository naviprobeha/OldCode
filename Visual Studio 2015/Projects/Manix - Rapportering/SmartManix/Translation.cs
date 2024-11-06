using System;

using System.Collections.Generic;
using System.Text;

namespace Navipro.SmartInventory
{
    public class Translation
    {
        public static string translate(string languageCode, string caption)
        {
            if (languageCode == "sv") return caption;
            if (languageCode == "en") return translateEn(caption);
            return caption;
        }

        private static string translateEn(string caption)
        {
            if (caption == "Letar efter uppdateringar...") return "Looking for updates...";
            if (caption == "Nedladdning klar.") return "Download complete.";
            if (caption == "Laddar ner ") return "Downloading ";
            if (caption == "Uppdatera") return "Update";
            if (caption == "Inleverans") return "Receive";
            if (caption == "Inventering") return "Phys. Inventory";
            if (caption == "Utleverans") return "Ship";
            if (caption == "Ange inköpsordernr.") return "Enter purchase order no.";
            if (caption == "Inköpsordernr:") return "Purchase Order No:";
            if (caption == "Avbryt") return "Cancel";
            if (caption == "OK") return "OK";
            if (caption == "Ansluter till ") return "Connecting to ";
            if (caption == "Skickar data...") return "Sending data...";
            if (caption == "Tar emot data...") return "Receiving data...";
            if (caption == "Kopplar ifrån...") return "Disconnecting...";
            if (caption == "Ordern ej funnen.") return "Order not found.";
            if (caption == "Scanna varje artikels EAN-kod.") return "Scan the EAN-code of every product.";
            if (caption == "EAN:") return "EAN-code:";
            if (caption == "Artikelnr") return "Item No.";
            if (caption == "Variantkod") return "Variant";
            if (caption == "Beskrivning") return "Description";
            if (caption == "Enhet") return "Unit";
            if (caption == "Antal") return "Quantity";
            if (caption == "Inlev. antal") return "Received";
            if (caption == "Antal att inlev.") return "To Rec.";
            if (caption == "Lev. antal") return "Shipped";
            if (caption == "Antal att lev.") return "To Ship";
            if (caption == "Det finns ingen artikel med den här EAN-koden på ordern:") return "The item with the EAN-code does not exist on the order:";
            if (caption == "Alla rader med den här artikeln är redan inlevererade:") return "All lines with this item are already received:";
            if (caption == "Alla rader med den här artikeln är redan levererade:") return "All lines with this item are already shipped:";
            if (caption == "Bokför") return "Post";
            if (caption == "Ändra") return "Edit";
            if (caption == "Det finns inga orderrader.") return "There are no order lines.";
            if (caption == "Det finns inga order i listan.") return "There are no orders in the list.";
            if (caption == "Fel") return "Error";
            if (caption == "Inleveransen bokförd.") return "Posting complete.";
            if (caption == "Leveransen bokförd.") return "Posting complete.";
            if (caption == "Vill du bokföra inleveransen?") return "Do you want to post the receipt?";
            if (caption == "Vill du bokföra leveransen?") return "Do you want to post the shipment?";
            if (caption == "Bokföring") return "Posting";
            if (caption == "Välj order.") return "Select order.";
            if (caption == "Välj") return "Select";
            if (caption == "Stäng") return "Close";
            if (caption == "Nr") return "No.";
            if (caption == "Kundnr") return "Customer No.";
            if (caption == "Namn") return "Name";
            if (caption == "Adress") return "Address";
            if (caption == "Ort") return "City";
            if (caption == "Land") return "Country";
            if (caption == "Orderdatum") return "Order Date";
            if (caption == "Totalt antal") return "Total Qty.";
            if (caption == "Ange antal") return "Enter Quantity";
            if (caption == "För hög kvantitet.") return "Quantity to high.";

            return caption;
        }
    }
}
