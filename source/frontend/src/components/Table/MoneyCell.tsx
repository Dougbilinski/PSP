import { FastCurrencyInput } from 'components/common/form/FastCurrencyInput';
import TooltipWrapper from 'components/common/TooltipWrapper';
import { useFormikContext } from 'formik';
import useKeycloakWrapper from 'hooks/useKeycloakWrapper';
import { IProperty } from 'interfaces';
import React from 'react';
import { CellProps } from 'react-table';
import { formatMoney } from 'utils';

interface IEditableCellProps {
  namespace: string;
  suppressValidation: boolean;
  cell: CellProps<IProperty, number | ''>;
}

export const EditableMoneyCell = ({
  namespace = 'properties',
  suppressValidation,
  cell,
}: IEditableCellProps) => {
  const context = useFormikContext();

  const property = cell.row.original;
  const canEdit = useKeycloakWrapper().canUserEditProperty(property);

  return canEdit ? (
    <>
      <FastCurrencyInput
        formikProps={context}
        suppressValidation={suppressValidation}
        field={`${namespace}.${cell.row.id}.${cell.column.id}`}
      ></FastCurrencyInput>
    </>
  ) : (
    <TooltipWrapper
      toolTipId={`${namespace}.${cell.row.id}.${cell.column.id}`}
      toolTip="You may only edit a property if it belongs to your organization."
    >
      <i>{cell.value === undefined || cell.value === '' ? '' : formatMoney(cell.value)}</i>
    </TooltipWrapper>
  );
};

export const MoneyCell = ({ cell: { value } }: CellProps<any, number | '' | undefined>) => (
  <div>{value === undefined || value === '' ? '' : formatMoney(value)}</div>
);

export const AsterixMoneyCell = ({ cell: { value } }: CellProps<any, number | '' | undefined>) => (
  <div>{value === undefined || value === '' ? '' : `${formatMoney(value)} *`}</div>
);
