﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OlympicFlights.Service.TicketManagement
{
    public class Ticket
    {
        /// <summary>
        /// Gets/sets ticket's ID
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// Gets/sets ticket's client ID
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets/sets ticket's flight ID
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Gets/sets is ticket's purchase approved
        /// </summary>
        public bool IsPurchaseApproved { get; set; }

        /// <summary>
        /// Gets/sets is ticket deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets/sets is ticket's purchase approved
        /// </summary>
        public DateTime Created { get; set; }
    }
}
