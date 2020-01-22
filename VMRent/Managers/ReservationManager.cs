using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMRent.Models;
using VMRent.Repositories;
using VMRent.Stores;

namespace VMRent.Managers
{
    public class ReservationManager
    {
        private readonly IUserVmRepository _userVmRepository;

        public ReservationManager(IUserVmRepository userVmRepository)
        {
            _userVmRepository = userVmRepository;
        }

        private static Task<bool> Overlap(DateTime startA, DateTime startB, DateTime endA, DateTime endB)
        {
            return Task.FromResult(startA < endB && startB < endA);
        }

        private Task<bool> IsReserved(Vm vm, DateTime startTime, DateTime endTime)
        {
            return Task.FromResult(_userVmRepository
                .GetAll(uv => uv.Vm.Id.Equals(vm.Id))
                .Any(uv => Overlap(uv.StartTime, startTime, uv.EndTime, endTime).Result));
        }
        
        public Task<IList<UserVm>> GetReservationsForUserAsync(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            IList<UserVm> list = _userVmRepository
                .GetAll(uv => uv.User.Id.Equals(user.Id))?.ToList();
            return Task.FromResult(list);
        }

        public Task<IList<UserVm>> GetReservationsForVmAsync(Vm vm)
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));

            IList<UserVm> list = _userVmRepository
                .GetAll(uv => uv.Vm.Id.Equals(vm.Id)).ToList();
            return Task.FromResult(list);
        }

        public async Task<UserVm> CreateReservationAsync(User user, Vm vm, DateTime? startTime, DateTime? endTime)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (vm is null) throw new ArgumentNullException(nameof(vm));
            if (!startTime.HasValue) throw new ArgumentNullException(nameof(startTime));
            if (!endTime.HasValue) throw new ArgumentNullException(nameof(endTime));

            if (!user.Active) 
                return await Task.FromResult<UserVm>(null);

            if (startTime.Value > endTime.Value)
            {
                throw new ArgumentException($"End time cannot be sooner than start time");
            }

            if (startTime.Value < DateTime.UtcNow)
            {
                throw new ArgumentException("Past time reservation");
            }

            if (await IsReserved(vm, startTime.Value, endTime.Value))
            {
                throw new ArgumentException($"Virtual machine with name {vm.Name} is already reserved");
            }

            var reservations = await GetReservationsForUserAsync(user);
            if (reservations.Count >= user.UserType.MaxReservations)
            {
                throw new ArgumentException($"You have reached reservations count limit - {reservations.Count}");
            }
            
            var userVm = new UserVm
            {
                User = user,
                Vm = vm,
                EndTime = endTime.Value,
                StartTime = startTime.Value
            };

            return await Task.FromResult(_userVmRepository.Add(userVm));
        }

        public Task CancelReservationAsync(UserVm userVm)
        {
            if (userVm is null) throw new ArgumentNullException(nameof(userVm));
            
            if (DateTime.UtcNow > userVm.EndTime)
                return Task.CompletedTask;
            if (userVm.User.Active)
                _userVmRepository.Delete(userVm);
            return Task.CompletedTask;
        }

        public Task<UserVm> GetReservationById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            return Task.FromResult(_userVmRepository.Get(id));
        }
    }
}