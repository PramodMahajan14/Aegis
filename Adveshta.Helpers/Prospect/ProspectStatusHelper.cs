using Adveshta.Utility.Common;

namespace Adveshta.Helpers.Prospect
{
    public class ProspectStatusHelper
    {
        private readonly Dictionary<Guid, List<Guid>> _statusTransitions;

        public ProspectStatusHelper()
        {
            _statusTransitions = new Dictionary<Guid, List<Guid>>
            {
                [ProspectsStatusMaster.NEW] = new()
            {
                ProspectsStatusMaster.ACTIVE,
                ProspectsStatusMaster.DISQUALIFIED
            },

                [ProspectsStatusMaster.ACTIVE] = new()
            {
                ProspectsStatusMaster.FOLLOW_UP,
                ProspectsStatusMaster.QUALIFICATION,
                ProspectsStatusMaster.DORMANT,
                ProspectsStatusMaster.DISQUALIFIED
            },

                [ProspectsStatusMaster.FOLLOW_UP] = new()
            {
                ProspectsStatusMaster.ACTIVE,
                ProspectsStatusMaster.QUALIFICATION,
                ProspectsStatusMaster.DORMANT,
                ProspectsStatusMaster.DISQUALIFIED
            },

                [ProspectsStatusMaster.QUALIFICATION] = new()
            {
                ProspectsStatusMaster.QUALIFIED,
                ProspectsStatusMaster.DISQUALIFIED,
                ProspectsStatusMaster.FOLLOW_UP
            },

                [ProspectsStatusMaster.QUALIFIED] = new()
            {
                ProspectsStatusMaster.CONVERTED,
                ProspectsStatusMaster.DISQUALIFIED
            },

                [ProspectsStatusMaster.DORMANT] = new()
            {
                ProspectsStatusMaster.ACTIVE,
                ProspectsStatusMaster.DISQUALIFIED
            },

                [ProspectsStatusMaster.DISQUALIFIED] = new()
            {
                ProspectsStatusMaster.ACTIVE
            },

                [ProspectsStatusMaster.CONVERTED] = new()
            };
        }

        public List<Guid> NextStatus(Guid currentStatus)
        {
            return _statusTransitions.TryGetValue(currentStatus, out var statuses)
                ? statuses
                : new List<Guid>();
        }
    }
}

// STATUS_TRANSITIONS = {
//   NEW:           ['ACTIVE', 'DISQUALIFIED'],
//   ACTIVE:        ['FOLLOW_UP', 'QUALIFICATION', 'DORMANT', 'DISQUALIFIED'],
//   FOLLOW_UP:     ['ACTIVE', 'QUALIFICATION', 'DORMANT', 'DISQUALIFIED'],
//   QUALIFICATION: ['QUALIFIED', 'DISQUALIFIED', 'FOLLOW_UP'],
//   QUALIFIED:     ['CONVERTED', 'DISQUALIFIED'],
//   DORMANT:       ['ACTIVE', 'DISQUALIFIED'],
//   DISQUALIFIED:  ['ACTIVE'],   // can reopen if have special permission
//   CONVERTED:     [],           // terminal
// }