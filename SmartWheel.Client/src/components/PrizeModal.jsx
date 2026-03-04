function PrizeModal({ prizeAmount, onClose }) {
  return (
    <div className="modal-overlay">
      <div className="modal-card prize-card">
        <h2>🎉 Congratulations!</h2>

        <p>You won</p>

        <div className="prize-amount">R{prizeAmount}</div>

        <button className="modal-primary-button" onClick={onClose}>
          Awesome!
        </button>
      </div>
    </div>
  );
}

export default PrizeModal;
