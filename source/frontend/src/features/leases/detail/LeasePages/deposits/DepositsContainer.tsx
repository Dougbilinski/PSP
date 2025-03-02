import GenericModal from 'components/common/GenericModal';
import { LeaseStateContext } from 'features/leases/context/LeaseContext';
import { apiLeaseToFormLease } from 'features/leases/leaseUtils';
import { Formik, getIn } from 'formik';
import { defaultLease } from 'interfaces';
import { IParentConcurrencyGuard } from 'interfaces/IParentConcurrencyGuard';
import { noop } from 'lodash';
import { Api_SecurityDeposit, Api_SecurityDepositReturn } from 'models/api/SecurityDeposit';
import { useContext, useState } from 'react';

import DepositNotes from './components/DepositNotes/DepositNotes';
import DepositsReceivedContainer from './components/DepositsReceivedContainer/DepositsReceivedContainer';
import DepositsReturnedContainer from './components/DepositsReturnedContainer/DepositsReturnedContainer';
import { useLeaseDepositReturns } from './hooks/useDepositReturns';
import { useLeaseDeposits } from './hooks/useDeposits';
import ReceivedDepositModal from './modal/receivedDepositModal/ReceivedDepositModal';
import ReturnedDepositModal from './modal/returnedDepositModal/ReturnedDepositModal';
import { FormLeaseDeposit } from './models/FormLeaseDeposit';
import { FormLeaseDepositReturn } from './models/FormLeaseDepositReturn';
import * as Styled from './styles';

export interface IDepositsContainerProps {}

export const DepositsContainer: React.FunctionComponent<
  React.PropsWithChildren<IDepositsContainerProps>
> = () => {
  const { lease, setLease } = useContext(LeaseStateContext);
  const securityDeposits: Api_SecurityDeposit[] = getIn(lease, 'securityDeposits') ?? [];
  const depositReturns: Api_SecurityDepositReturn[] = securityDeposits.flatMap(
    x => x.depositReturns,
  );
  const [editNotes, setEditNotes] = useState<boolean>(false);

  const [showDepositEditModal, setShowEditModal] = useState<boolean>(false);
  const [deleteModalWarning, setDeleteModalWarning] = useState<boolean>(false);
  const [deleteReturnModalWarning, setDeleteReturnModalWarning] = useState<boolean>(false);

  const [showReturnEditModal, setShowReturnEditModal] = useState<boolean>(false);

  const [depositToDelete, setDepositToDelete] = useState<FormLeaseDeposit | undefined>(undefined);
  const [editDepositValue, setEditDepositValue] = useState<FormLeaseDeposit>(
    FormLeaseDeposit.createEmpty(),
  );

  const [depositReturnToDelete, setDepositReturnToDelete] = useState<
    FormLeaseDepositReturn | undefined
  >(undefined);
  const [editReturnValue, setEditReturnValue] = useState<FormLeaseDepositReturn | undefined>(
    undefined,
  );

  const { updateLeaseDeposit, updateLeaseDepositNote, removeLeaseDeposit } = useLeaseDeposits();
  const { updateLeaseDepositReturn, removeLeaseDepositReturn } = useLeaseDepositReturns();

  const onAddDeposit = () => {
    setEditDepositValue(FormLeaseDeposit.createEmpty());
    setShowEditModal(true);
  };

  const onEditDeposit = (id: number) => {
    var deposit = securityDeposits.find(x => x.id === id);
    if (deposit) {
      setEditDepositValue(FormLeaseDeposit.fromApi(deposit));
      setShowEditModal(true);
    }
  };

  const onDeleteDeposit = (id: number) => {
    var deposit = securityDeposits.find(x => x.id === id);
    if (deposit) {
      setDepositToDelete(FormLeaseDeposit.fromApi(deposit));
      setDeleteModalWarning(true);
    }
  };

  const onReturnDeposit = (id: number) => {
    var deposit = securityDeposits.find(x => x.id === id);
    if (deposit) {
      setEditReturnValue(FormLeaseDepositReturn.createEmpty(deposit));
      setShowReturnEditModal(true);
    }
  };

  const onDeleteDepositConfirmed = async () => {
    if (lease && lease.id && lease.rowVersion && depositToDelete) {
      const updatedLease = await removeLeaseDeposit({
        parentId: lease.id,
        parentRowVersion: lease.rowVersion,
        payload: depositToDelete.toApi(),
      });
      if (!!updatedLease?.id) {
        setDepositToDelete(undefined);
        setDeleteModalWarning(false);
        setLease(updatedLease);
      }
    }
  };

  const onDeleteDepositReturnConfirmed = async () => {
    if (lease && lease.id && lease.rowVersion && depositReturnToDelete) {
      const updatedLease = await removeLeaseDepositReturn({
        parentId: lease.id,
        parentRowVersion: lease.rowVersion,
        payload: depositReturnToDelete.toInterfaceModel(),
      });
      if (!!updatedLease?.id) {
        setDepositReturnToDelete(undefined);
        setDeleteReturnModalWarning(false);
        setLease(updatedLease);
      }
    }
  };

  /**
   * Send the save request (either an update or an add). Use the response to update the parent lease.
   * @param depositForm
   */
  const onSaveDeposit = async (depositForm: FormLeaseDeposit) => {
    if (lease && lease.id && lease.rowVersion) {
      let request: IParentConcurrencyGuard<Api_SecurityDeposit> = {
        parentId: lease.id,
        parentRowVersion: lease.rowVersion,
        payload: depositForm.toApi(),
      };
      const updatedLease = await updateLeaseDeposit(request);
      if (!!updatedLease?.id) {
        setLease(updatedLease);
        setEditDepositValue(FormLeaseDeposit.createEmpty());
        setShowEditModal(false);
      }
    } else {
      console.error('Lease information incomplete');
    }
  };

  const onEditReturnDeposit = (id: number) => {
    var deposit = depositReturns.find(x => x.id === id);
    if (deposit) {
      var parentDeposit = securityDeposits.find(x => x.id === deposit?.parentDepositId);
      if (parentDeposit) {
        setEditReturnValue(FormLeaseDepositReturn.createFromModel(deposit, parentDeposit));
        setShowReturnEditModal(true);
      } else {
        console.error('Parent deposit incomplete');
      }
    }
  };

  const onDeleteDepositReturn = (id: number) => {
    var deposit = depositReturns.find(x => x.id === id);
    if (deposit) {
      var parentDeposit = securityDeposits.find(x => x.id === deposit?.parentDepositId);
      if (parentDeposit) {
        setDepositReturnToDelete(FormLeaseDepositReturn.createFromModel(deposit, parentDeposit));
        setDeleteReturnModalWarning(true);
      } else {
        console.error('Parent deposit incomplete');
      }
    }
  };

  /**
   * Send the save request (either an update or an add). Use the response to update the parent lease.
   * @param returnDepositForm
   */
  const onSaveReturnDeposit = async (returnDepositForm: FormLeaseDepositReturn) => {
    if (lease && lease.id && lease.rowVersion) {
      let request: IParentConcurrencyGuard<Api_SecurityDepositReturn> = {
        parentId: lease.id,
        parentRowVersion: lease.rowVersion,
        payload: returnDepositForm.toInterfaceModel(),
      };
      const updatedLease = await updateLeaseDepositReturn(request);
      if (!!updatedLease?.id) {
        setLease(updatedLease);
        setDepositReturnToDelete(undefined);
        setShowReturnEditModal(false);
      }
    } else {
      console.error('Lease information incomplete');
    }
  };

  const initialValues = apiLeaseToFormLease(lease);

  return (
    <Formik initialValues={{ ...defaultLease, initialValues }} onSubmit={noop}>
      {formikProps => (
        <Styled.DepositsContainer>
          <DepositsReceivedContainer
            securityDeposits={securityDeposits}
            onAdd={onAddDeposit}
            onEdit={onEditDeposit}
            onDelete={onDeleteDeposit}
            onReturn={onReturnDeposit}
          />

          <DepositsReturnedContainer
            securityDeposits={securityDeposits}
            depositReturns={depositReturns}
            onEdit={onEditReturnDeposit}
            onDelete={onDeleteDepositReturn}
          />

          <DepositNotes
            disabled={!editNotes}
            onEdit={() => setEditNotes(true)}
            onSave={async (notes: string) => {
              const updatedLease = await updateLeaseDepositNote({
                payload: { note: notes },
                parentId: lease?.id,
                parentRowVersion: lease?.rowVersion,
              } as IParentConcurrencyGuard<{ note: string }>);
              if (updatedLease?.id) {
                setLease(updatedLease);
                setEditNotes(false);
              }
              return updatedLease;
            }}
            onCancel={() => {
              setEditNotes(false);
              formikProps.setFieldValue('returnNotes', lease?.returnNotes ?? '');
            }}
          />

          <GenericModal
            display={deleteModalWarning}
            title="Delete Deposit"
            message={`Are you sure you want to remove the deposit?`}
            handleOk={() => onDeleteDepositConfirmed()}
            okButtonText="OK"
            closeButton
            setDisplay={setDeleteModalWarning}
          />
          <GenericModal
            display={deleteReturnModalWarning}
            title="Delete Deposit Return"
            message={`Are you sure you want to remove this deposit return?`}
            handleOk={() => onDeleteDepositReturnConfirmed()}
            okButtonText="OK"
            closeButton
            setDisplay={setDeleteReturnModalWarning}
          />

          <ReceivedDepositModal
            display={showDepositEditModal}
            initialValues={editDepositValue}
            onCancel={() => {
              setEditDepositValue(FormLeaseDeposit.createEmpty());
              setShowEditModal(false);
            }}
            onSave={onSaveDeposit}
          />

          <ReturnedDepositModal
            display={showReturnEditModal}
            initialValues={editReturnValue}
            onCancel={() => {
              setEditReturnValue(undefined);
              setShowReturnEditModal(false);
            }}
            onSave={onSaveReturnDeposit}
          />
        </Styled.DepositsContainer>
      )}
    </Formik>
  );
};

export default DepositsContainer;
