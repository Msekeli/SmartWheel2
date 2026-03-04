function MessageModal({ message, onClose }) {
  return (
    <div className="modal-overlay">
      <div className="modal-card message-card">
        <h2 className="message-title">Notice</h2>

        <p className="message-text">{message}</p>

        <button className="modal-primary-button" onClick={onClose}>
          OK
        </button>
      </div>
    </div>
  );
}

export default MessageModal;
