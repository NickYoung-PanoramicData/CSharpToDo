export function createModal(id, options) {
	return new bootstrap.Modal(document.getElementById(id), options);
}
export function focus(id) {
	setTimeout(() => {
		var el = document.getElementById(id);
		if (el) {
			el.focus();
		}
	}, 10);
}