import Swal from 'sweetalert2'
class Alerts {
  async confirmAction(title = 'Are you sure?', text = "You won't be able to revert this!") {
    try {
      const res = await Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      })
      console.log(res)
      if (res.isConfirmed) {
        return true
      }
      return false
    } catch (error) {
      console.error(error)
    }
  }
}

export const alerts = new Alerts()
